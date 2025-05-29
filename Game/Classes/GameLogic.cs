using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    internal class GameLogic
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Player CurrentPlayer { get; set; }
        public Dice Dice { get; set; }
        //public Board Board { get; set; }

        public GameLogic(string playerName1, string playerName2)
        {
            Player1 = new Player(playerName1, Color.Black);
            Player2 = new Player(playerName2, Color.Red);
            CurrentPlayer = Player1;
        }

        public void NextMove()
        {
            if (CurrentPlayer == Player1)
            {
                CurrentPlayer = Player2;
            }
            else
            {
                CurrentPlayer = Player1;
            }
            Dice.ResettingValues();
        }

        public void RollTheDice() => Dice.Roll();

        public bool CanMove()
        {
            // Проверяем, может ли текущий игрок сделать ход

            return true;
        }
    }
}
