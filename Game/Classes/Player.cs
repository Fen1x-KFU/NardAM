namespace Game
{
    internal class Player
    {
        /// <summary>
        /// Имя игрока
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Цвет игрока
        /// </summary>
        public Color Color { get; set; }
        /// <summary>
        /// Сколько шашек дома
        /// </summary>
        public int ChipsAtHome { get; set; }
        /// <summary>
        /// Ход
        /// </summary>
        public bool Move { get; set; }
        /// <summary>
        /// Базовый конструктор класса
        /// </summary>
        /// <param name="name"></param>
        /// <param name="color"></param>
        public Player(string name, Color color)
        {
            Name = name;
            Color = color;
            ChipsAtHome = 0;
            Move = false;
        }
    }
}
