using Guna.UI2.WinForms;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using System.Windows.Forms;

namespace Game
{
    public partial class MainForm : Form
    {
        private AppDbContext db = new AppDbContext();
        private UserGame user_this;
        private UserGame user2;
        private Player pl_this;
        private Player pl2;
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;


        public MainForm(UserGame us)
        {
            InitializeComponent();

            this.user_this = us; 

            var existingPlayer = db.Players.FirstOrDefault(p => p.UserId == us.Id);

            if (existingPlayer == null)
            {

                this.pl_this = new Player
                {
                    UserGame = db.Users.Find(us.Id),
                    UserId = us.Id,
                    Name = us.Name
                };

                db.Players.Add(pl_this);
                db.SaveChanges();
            }
            else
            {
                this.pl_this = existingPlayer;
            }

            board.Visible = false;
            cube1.Visible = false;
            cube2.Visible = false;
            cube3.Visible = false;
            cube4.Visible = false;
            btn_Roll.Visible = false;
        }

        public async void btn_Ready_Click(object sender, EventArgs e)
        {
            btn_Ready.Enabled = false;

            try
            {
                // Обновляем статус в БД
                using (var freshDb = new AppDbContext()) // Новый контекст!
                {
                    var currentUser = await freshDb.Users.FindAsync(user_this.Id);
                    if (currentUser != null)
                    {
                        currentUser.IsReady = true;
                        await freshDb.SaveChangesAsync();
                    }
                }

                // Ожидаем готовность соперника
                bool opponentReady = await WaitForOpponentReady();
                user2 = db.Users.FirstOrDefault(u => u.Id != user_this.Id);

                if (opponentReady)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        board.Visible = true;
                        btn_Ready.Visible = false;
                        CreateChips(Color.Red);
                        CreateButtonMove();
                        lPlayer.Text = $"Игрок: {user_this.Name}";
                        lPlayer2.Text = $"Противник: {user2.Name}";
                        btn_Roll.Visible = true;
                    });
                }
                else
                {
                    MessageBox.Show("Соперник не подключился");
                    btn_Ready.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("В базе данных только один пользователь!");
                btn_Ready.Enabled = true;
            }
        }

        private async Task<bool> WaitForOpponentReady()
        {
            using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30)))
            {
                while (!cts.IsCancellationRequested)
                {
                    using (var freshDb = new AppDbContext()) // Новый контекст для каждой проверки
                    {
                        var opponent = await freshDb.Users
                            .AsNoTracking() // Для оптимизации
                            .FirstOrDefaultAsync(u => u.Id != user_this.Id);

                        if (opponent?.IsReady == true)
                            return true;
                    }

                    await Task.Delay(1000, cts.Token);
                }
                return false;
            }
        }


        private string _baseDir = AppDomain.CurrentDomain.BaseDirectory;

        private List<Button> _buttonsMove = new List<Button>();
        private List<Guna2CircleButton> _chips = new List<Guna2CircleButton>();


        private void CreateChips(Color color_this)
        {
            int chipSize = 32;       // Размер фишки
            int overlap = chipSize / 4;

            for (int i = 0; i < 15; i++)
            {
                Guna2CircleButton chip = new Guna2CircleButton();
                chip.Width = chipSize;
                chip.Height = chipSize;
                chip.FillColor = color_this;
                chip.BackColor = Color.Transparent;

                chip.Location = new Point(
                    board.Left,
                    board.Top + board.Height / 2 - chipSize - i * 10
                    );

                board.Controls.Add(chip);
                chip.BringToFront();
            }

            for (int i = 0; i < 15; i++)
            {
                Guna2CircleButton chip = new Guna2CircleButton();
                chip.Width = chipSize;
                chip.Height = chipSize;
                chip.FillColor = Color.Black;
                chip.BackColor = Color.Transparent;

                chip.Location = new Point(
                    board.Right - 118,
                    board.Top - board.Height / 2 + chipSize + i * 10
                    );

                board.Controls.Add(chip);
                chip.BringToFront();
            }
        }

        private void CreateButtonMove()
        {
            for (int i = 0; i < 6; i++)
            {
                var but = new Button()
                {
                    Name = $"but{i + 13}",
                    Height = 40,
                    Width = 40,
                    Location = new Point(
                        board.Left + 38 + i * 67,
                        board.Top - 50
                    ),
                    BackColor = Color.White,
                    Text = $"{i + 13}"
                };

                this.Controls.Add(but);
                but.BringToFront();
            }

            for (int i = 0; i < 6; i++)
            {
                var but = new Button()
                {
                    Name = $"but{i + 19}",
                    Height = 40,
                    Width = 40,
                    Location = new Point(
                        board.Left + 38 + (i + 6) * 67 + 55,
                        board.Top - 50
                    ),
                    BackColor = Color.White,
                    Text = $"{i + 19}"
                };

                this.Controls.Add(but);
                but.BringToFront();
            }

            for (int i = 0; i < 6; i++)
            {
                var but = new Button()
                {
                    Name = $"but{i + 1}",
                    Height = 40,
                    Width = 40,
                    Location = new Point(
                        board.Left + 38 + i * 67,
                        board.Top + board.Height + 18
                    ),
                    BackColor = Color.White,
                    Text = $"{i + 1}"
                };

                this.Controls.Add(but);
                but.BringToFront();
            }

            for (int i = 0; i < 6; i++)
            {
                var but = new Button()
                {
                    Name = $"but{i + 7}",
                    Height = 40,
                    Width = 40,
                    Location = new Point(
                        board.Left + 38 + (i + 6) * 67 + 55,
                        board.Top + board.Height + 18
                    ),
                    BackColor = Color.White,
                    Text = $"{i + 7}"
                };

                this.Controls.Add(but);
                but.BringToFront();
            }

        }

        public async void btn_RollStart_Click(object sender, EventArgs e)
        {
            btn_Roll.Enabled = false;
            db = new AppDbContext();

            this.pl2 = db.Players.FirstOrDefault(p => p.Id != pl_this.Id);

            pl_this.DicePlayer.Roll();
            db.SaveChanges();

            cube2.Visible = true;
            cube3.Visible = true;

            var image1 = Path.Combine(baseDir,
                "..",
                "..",
                "..",
                "Images",
                $"Cube{pl_this.DicePlayer.Value1}.jpg");
            var image2 = Path.Combine(baseDir,
                "..",
                "..",
                "..",
                "Images",
                $"Cube{pl2.DicePlayer.Value1}.jpg");

            var dicep = pl_this.DicePlayer.Value1;
            var dicep2 = pl2.DicePlayer.Value1;

            try
            {
                using (var freshDb = new AppDbContext())
                {
                    var currentUser = await freshDb.Players.FindAsync(pl_this.Id);
                    if (currentUser != null)
                    {
                        currentUser.DicePlayer.Roll();
                        await freshDb.SaveChangesAsync();
                    }
                }
                var opponentStart = await WaitForOpponentStart();

                if (opponentStart != 0)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        LoadImageToBox(cube2, image1);
                        LoadImageToBox(cube3, image2);
                        if (dicep > dicep2)
                        {
                            MessageBox.Show("Вы начинаете");
                            btn_Roll.Click -= btn_RollStart_Click;
                            btn_Roll.Click += btn_RollNew_Click;
                            btn_Roll.Enabled = true;
                            cube2.BackgroundImage = null;
                            cube3.BackgroundImage = null;
                        }
                        else if (dicep < dicep2)
                        {
                            MessageBox.Show("Противник начинает");
                            btn_Roll.Click -= btn_RollStart_Click;
                            btn_Roll.Click += btn_RollNew_Click;
                            btn_Roll.Enabled = true;
                            cube2.BackgroundImage = null;
                            cube3.BackgroundImage = null;
                            cube2.Visible = false;
                            cube3.Visible = false;
                        }
                        else
                        {
                            MessageBox.Show("Надо подбросить заново кубик");
                            btn_Roll.Enabled = true;
                        }
                    });
                }
                else
                {
                    MessageBox.Show("Соперник не бросил кубик ");
                    btn_Roll.Enabled = true;
                }
            
            }
            catch (Exception ex)
            {

            }

        }

        private async Task<int> WaitForOpponentStart()
        {
            using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30)))
            {
                while (!cts.IsCancellationRequested)
                {
                    using (var freshDb = new AppDbContext()) // Новый контекст для каждой проверки
                    {
                        var opponent = await freshDb.Players
                            .AsNoTracking() // Для оптимизации
                            .FirstOrDefaultAsync(u => u.Name == user2.Name);

                        if (opponent?.DicePlayer.Value1 != 0)
                            return opponent.DicePlayer.Value1;
                    }

                    await Task.Delay(1000, cts.Token);
                }
                return 0;
            }
        }

        private async void btn_RollNew_Click(object sender, EventArgs e)
        {

        }

        private void LoadImageToBox(PictureBox pictureBox, string imagePath)
        {
            if (pictureBox == null) return;

            if (!File.Exists(imagePath))
            {
                MessageBox.Show($"Файл не найден: {imagePath}");
                return;
            }

            try
            {
                pictureBox.Image?.Dispose(); // Освобождаем старое изображение
                pictureBox.Image = new Bitmap(imagePath);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}");
            }
        }
    }
}
