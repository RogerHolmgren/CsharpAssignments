using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaFighter
{
    class Round
    {
        public bool tie { get; internal set; }

        public int playerDiceRoll { get; internal set; }
        public int playerStrength { get; internal set; }

        public int enemyDiceRoll { get; internal set; }
        public int enemyStrength { get; internal set; }

        public string winnerName { get; internal set; }
        public int winnerDamage { get; internal set; }
        public string loserName { get; internal set; }
        public int loserHealthAfter { get; internal set; }

        public Round ()
        {
            this.tie = false;
        }
    }
}
