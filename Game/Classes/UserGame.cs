using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    internal class UserGame
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Reiting { get; set; } 
        public bool IsReady { get; set; }

        public UserGame(UserGame user)
        {
            this.Id = user.Id;
            this.Name = user.Name;
            this.Password = user.Password;
            this.Reiting = user.Reiting;
            this.IsReady = user.IsReady;
        }

        public UserGame()
        {
            this.IsReady = false;
        }
    }
}
