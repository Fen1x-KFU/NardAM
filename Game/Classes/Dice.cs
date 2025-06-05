namespace Game
{
    internal class Dice
    {
        /// <summary>
        /// Класс с рандомом (для выпадения)
        /// </summary>
        public Random random = new Random();
        /// <summary>
        /// Значение 1
        /// </summary>
        public int Value1 { get; set; }
        /// <summary>
        /// Значение 2
        /// </summary>
        public int Value2 { get; set; }
        /// <summary>
        /// Проверка на выпадение одинаковых значений
        /// </summary>
        public bool IsDouble() => Value1 == Value2;
        public void Roll()
        {
            Value1 = random.Next(1, 7);
            Value2 = random.Next(1, 7);
        }

        public void ResettingValues() => Value1 = Value2 = 0;
    }
}
