namespace Game
{
    internal class Board
    {
        // 24 точки + дом каждого игрока + бар (для выброшенных шашек)
        public int[] Points { get; private set; } = new int[26]; // 0-23 - точки, 24 - дом белых, 25 - дом черных
        public int[] Bar { get; private set; } = new int[2]; // 0 - белые на баре, 1 - черные на баре

        public void Initialize()
        {
            // Расставляем шашки по начальной позиции
            Points[0] = 2;
            Points[5] = -5;
            Points[7] = -3;
            Points[11] = 5;
            Points[12] = -5;
            Points[16] = 3;
            Points[18] = 5;
            Points[23] = -2;

            // Можно добавить метод для отладки
        }

        public bool IsValidMove(int from, int to, Player player, Dice dice)
        {
            // Здесь будет логика проверки корректности хода
            return true;
        }

        public void MoveChip(int from, int to, Player player)
        {
            // Логика перемещения шашки
        }
    }
}