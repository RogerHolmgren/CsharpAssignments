using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaFighter
{
    class Battle
    {
        public Character player { get; private set; }
        public Character enemy { get; private set; }

        public Battle(Character player, Character enemy)
        {
            this.player = player;
            this.enemy = enemy;
        }

        internal Round Fight()
        {
            Random rnd = new Random();
            int playerFightValue = player.strength + rnd.Next(1,6);


            return new Round();
        }
    }
}
