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
        public List<Round> Rounds { get; private set; }

        public Battle(Character player, Character enemy)
        {
            this.player = player;
            this.enemy = enemy;
            this.Rounds = new List<Round>();
        }

        /// <summary>
        /// If either player or enemy is not alive, battle is done!
        /// </summary>
        /// <returns></returns>
        internal bool IsDone()
        {
            return !player.IsAlive() || !enemy.IsAlive() || player.IsRetired;
        }



        internal void saveToLog(Round round)
        {
            Rounds.Add(round);
        }
    }
}
