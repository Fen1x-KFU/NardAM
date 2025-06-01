using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using System.Windows.Forms;

namespace Game
{
    internal partial class MainForm : Form
    {
        private AppDbContext db = new AppDbContext();
        private UserGame user_this;
        private UserGame user2;
        private Player player_this;
        private Player player2;


        public MainForm(UserGame us)
        {
            InitializeComponent();

            this.user_this = us;
            user2 = new UserGame(db.Users.FirstOrDefault(u => u.Id != us.Id));
            player_this =  new Player(user_this.Name, Color.Black);
            player2 = new Player(user2.Name, Color.Red);
            //MessageBox.Show(player_this.Name);
            //MessageBox.Show(player2.Name);

            board.Visible = false;
        }

        private async void btn_Ready_Click(object sender, EventArgs e)
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

                if (opponentReady)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        board.Visible = true;
                        btn_Ready.Visible = false;
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
                MessageBox.Show($"Ошибка: {ex.Message}");
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
                            .FirstOrDefaultAsync(u => u.Id == user2.Id);

                        if (opponent?.IsReady == true)
                            return true;
                    }

                    await Task.Delay(1000, cts.Token);
                }
                return false;
            }
        }

    }
}
