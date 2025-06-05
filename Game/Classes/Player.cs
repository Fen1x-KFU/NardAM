using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Game
{
    public class Player
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserGame UserGame { get; set; }
        public Dice DicePlayer {  get; set; }
        public string Name { get; set; }
        public bool Move { get; set; }

        public Player()
        {
            DicePlayer = new Dice();
            DicePlayer.Value1 = 0;
            DicePlayer.Value2 = 0;
            Move = false;
        }
    }
}
