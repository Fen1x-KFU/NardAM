namespace Game
{
    public partial class MainForm : Form
    {
        private GameLogic game;

        public MainForm()
        {
            InitializeComponent();
            game = new GameLogic("����� 1", "����� 2");

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
