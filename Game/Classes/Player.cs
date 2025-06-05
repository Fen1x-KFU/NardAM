namespace Game
{
    internal class Player
    {
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UserGame UserGame { get; set; }
        /// <summary>
        /// Кубики игрока
        /// </summary>
        public Dice DicePlayer {  get; set; }
        /// <summary>
        /// Имя игрока
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Ход
        /// </summary>
        public bool Move { get; set; }
        /// <summary>
        /// Базовый конструктор класса
        /// </summary>
        /// <param name="name"></param>
        public Player()
        {
            DicePlayer.Value1 = 0;
            DicePlayer.Value2 = 0;
            Move = false;
        }
    }
}
