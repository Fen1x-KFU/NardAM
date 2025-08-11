using Guna.UI2.WinForms;
using Microsoft.EntityFrameworkCore;

namespace Game
{
    public partial class MainForm : Form
    {
        private UserGame user_this;
        private UserGame user2;
        private Player pl_this;
        private Player pl2;
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;


        public MainForm(UserGame us)
        {
            InitializeComponent();

            this.user_this = us;

            // ��������� ������ � ��
            using (var freshDb = new AppDbContext()) // ����� ��������!
            {
                var existingPlayer = freshDb.Players.FirstOrDefault(p => p.UserId == us.Id);

                if (existingPlayer == null)
                {

                    this.pl_this = new Player
                    {
                        UserGame = freshDb.Users.Find(us.Id),
                        UserId = us.Id,
                        Name = us.Name
                    };

                    freshDb.Players.Add(pl_this);
                    freshDb.SaveChanges();
                }
                else
                {
                    this.pl_this = existingPlayer;
                }
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
                // 1. ��������� ������ � ����� ���������
                using (var updateDb = new AppDbContext())
                {
                    var currentUser = await updateDb.Users.FindAsync(user_this.Id);
                    if (currentUser != null)
                    {
                        currentUser.IsReady = true;
                        await updateDb.SaveChangesAsync();
                    }
                }

                // 2. ������� ���������
                bool opponentReady = await WaitForOpponentReady();

                using (var db = new AppDbContext())
                {
                    user2 = await db.Users.FirstOrDefaultAsync(u => u.Id != user_this.Id);
                }

                // 4. ��������� UI
                if (opponentReady && user2 != null)
                {
                    BeginInvoke((MethodInvoker)delegate
                    {
                        board.Visible = true;
                        btn_Ready.Visible = false;
                        CreateChips(Color.Red);
                        CreateButtonMove();
                        lPlayer.Text = $"�����: {user_this.Name}";
                        lPlayer2.Text = $"���������: {user2.Name}";
                        btn_Roll.Visible = true;
                    });
                }
                else
                {
                    MessageBox.Show("�������� �� �����������");
                    btn_Ready.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������: {ex.Message}");
                btn_Ready.Enabled = true;
            }
        }

        private async Task<bool> WaitForOpponentReady()
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
            try
            {
                while (!cts.IsCancellationRequested)
                {
                    using (var freshDb = new AppDbContext())
                    {
                        var opponent = await freshDb.Users
                            .AsNoTracking()
                            .FirstOrDefaultAsync(u => u.Id != user_this.Id && u.IsReady, cts.Token);

                        if (opponent != null)
                            return true;
                    }

                    await Task.Delay(1000, cts.Token).ConfigureAwait(false);
                }
            }
            catch (OperationCanceledException)
            {
                return false;
            }
            return false;
        }

        public async void btn_RollStart_Click(object sender, EventArgs e)
        {
            btn_Roll.Enabled = false;

            try
            {
                // �������� ������ ��������� ������ ������ �� ��
                using (var db = new AppDbContext())
                {
                    var currentUser = await db.Players
                        .Include(p => p.DicePlayer)
                        .FirstOrDefaultAsync(p => p.UserId == user_this.Id);

                    if (currentUser == null)
                    {
                        MessageBox.Show("�� �� ������� � ���� ������.");
                        return;
                    }

                    // ������� �����
                    currentUser.DicePlayer.Roll();

                    await db.SaveChangesAsync();

                    // ���������� ���������
                    int myRoll = currentUser.DicePlayer.Value1;

                    // ���� � ��������
                    var imageMy = Path.Combine(baseDir, "..", "..", "..", "Images", $"Cube{myRoll}.jpg");

                    // ��� ����� ���������
                    int opponentRoll = await WaitForOpponentStart();

                    if (opponentRoll == 0)
                    {
                        MessageBox.Show("�������� �� ������ �����.");
                        btn_Roll.Enabled = true;
                        return;
                    }

                    var imageOpponent = Path.Combine(baseDir, "..", "..", "..", "Images", $"Cube{opponentRoll}.jpg");

                    // ��������� ���������
                    this.Invoke((MethodInvoker)delegate
                    {
                        LoadImageToBox(cube2, imageMy);
                        LoadImageToBox(cube3, imageOpponent);

                        cube2.Visible = true;
                        cube3.Visible = true;

                        if (myRoll > opponentRoll)
                        {
                            MessageBox.Show("�� ���������");
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
                            MessageBox.Show("�������� ��������");
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
                            MessageBox.Show("��������� ������. ������� ������.");
                            btn_Roll.Enabled = true;
                            currentUser.DicePlayer.ResettingValues();
                            db.SaveChanges();
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"��������� ������: {ex.Message}");
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
                MessageBox.Show("����� ���� �� ������ �����");
            }

            return 0;
        }

        private async void btn_RollNew_Click(object sender, EventArgs e)
        {

        }
    }
}
