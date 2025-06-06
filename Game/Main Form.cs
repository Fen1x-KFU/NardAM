using Guna.UI2.WinForms;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using System.Runtime.CompilerServices;
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

            try
            {
                // Получаем свежий экземпляр своего игрока из БД
                using (var db = new AppDbContext())
                {
                    var currentUser = await db.Players
                        .Include(p => p.DicePlayer)
                        .FirstOrDefaultAsync(p => p.UserId == user_this.Id);

                    if (currentUser == null)
                    {
                        MessageBox.Show("Вы не найдены в базе данных.");
                        return;
                    }

                    // Бросаем кубик
                    currentUser.DicePlayer.Roll();

                    await db.SaveChangesAsync();

                    // Запоминаем результат
                    int myRoll = currentUser.DicePlayer.Value1;

                    // Путь к картинке
                    var imageMy = Path.Combine(baseDir, "..", "..", "..", "Images", $"Cube{myRoll}.jpg");

                    // Ждём кубик соперника
                    int opponentRoll = await WaitForOpponentStart();

                    if (opponentRoll == 0)
                    {
                        MessageBox.Show("Соперник не бросил кубик.");
                        btn_Roll.Enabled = true;
                        return;
                    }

                    var imageOpponent = Path.Combine(baseDir, "..", "..", "..", "Images", $"Cube{opponentRoll}.jpg");

                    // Обновляем интерфейс
                    this.Invoke((MethodInvoker)delegate
                    {
                        LoadImageToBox(cube2, imageMy);
                        LoadImageToBox(cube3, imageOpponent);

                        cube2.Visible = true;
                        cube3.Visible = true;

                        if (myRoll > opponentRoll)
                        {
                            MessageBox.Show("Вы начинаете");
                            btn_Roll.Click -= btn_RollStart_Click;
                            btn_Roll.Click += btn_RollNew_Click;
                            btn_Roll.Enabled = true;
                            cube2.Image = null;
                            cube3.Image = null;
                            currentUser.Move = true;
                            db.SaveChanges();
                        }
                        else if (myRoll < opponentRoll)
                        {
                            MessageBox.Show("Соперник начинает");
                            btn_Roll.Click -= btn_RollStart_Click;
                            btn_Roll.Click += btn_RollNew_Click;
                            btn_Roll.Enabled = false;
                            cube2.Image = null;
                            cube3.Image = null;
                            cube2.Visible = false;
                            cube3.Visible = false;
                            currentUser.Move = false; 
                            db.SaveChanges();
                        }
                        else
                        {
                            MessageBox.Show("Результат совпал. Бросьте заново.");
                            btn_Roll.Enabled = true;
                            currentUser.DicePlayer.ResettingValues();
                            db.SaveChanges();
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
                btn_Roll.Enabled = true;
            }
        }

        private async Task<int> WaitForOpponentStart()
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));

            try
            {
                while (!cts.IsCancellationRequested)
                {
                    await using (var freshDb = new AppDbContext())
                    {
                        var diceValue = await freshDb.Players
                            .Where(u => u.Name == user2.Name)
                            .Select(u => u.DicePlayer.Value1)
                            .FirstOrDefaultAsync(cts.Token);

                        if (diceValue != 0)
                        {
                            return diceValue;
                        }
                    }

                    await Task.Delay(1000, cts.Token);
                }
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Игрок пока не бросил кубик");
            }

            return 0;
        }

        private async void btn_RollNew_Click(object sender, EventArgs e)
        {
            db = new AppDbContext();

            var pl = db.Players.FirstOrDefault(u =>  u.Name == user_this.Name);
            var pl2 = db.Players.FirstOrDefault(u =>  u.Name == user2.Name);
            pl2.Move = true;
            pl.Move = false;


            db.SaveChanges();
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

        //private void MainForm_FormClosing(object sender, FormClosingEventArgs e) => Application.Exit();
    }
}
