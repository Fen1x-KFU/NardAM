namespace Game
{
    public partial class MainForm : Form
    {
        private GameLogic game;

        public MainForm()
        {
            InitializeComponent();
            game = new GameLogic("Игрок 1", "Игрок 2");

        }

        //private void btnRoll_Click(object sender, EventArgs e)
        //{
        //    game.RollDice();
        //    lblDice.Text = $"{game.Dice.Value1} - {game.Dice.Value2}";
        //    // Здесь можно обновить UI
        //}

        //private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        //{
        //    // Определить, куда нажали
        //    // Передать координаты в GameLogic
        //}
    }
}
