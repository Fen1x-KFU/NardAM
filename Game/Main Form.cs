using Microsoft.VisualBasic.ApplicationServices;

namespace Game
{
    public partial class MainForm : Form
    {
        private GameLogic game;
        //private AppDbContext db;
        //private UserGame us;

        public MainForm()
        {
            InitializeComponent();
            game = new GameLogic("����� 1", "����� 2");
            //db = new AppDbContext();
            //us.Name = "Andrey";
            //us.Password = "4826";
            //us.Reiting = 780;
            //db.Users.Add(us);
        }

        //private void btnRoll_Click(object sender, EventArgs e)
        //{
        //    game.RollDice();
        //    lblDice.Text = $"{game.Dice.Value1} - {game.Dice.Value2}";
        //    // ����� ����� �������� UI
        //}

        //private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        //{
        //    // ����������, ���� ������
        //    // �������� ���������� � GameLogic
        //}
    }
}
