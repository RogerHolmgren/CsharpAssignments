using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaFighter
{
    class Round
    {
        private string winner;
        public Character player { get; private set; }
        public Character enemy { get; private set; }
        public bool tie { get; internal set; } = false;

        public int playerDieRoll { get; internal set; }
        public int enemyDieRoll { get; internal set; }

        private Round(Character player, Character enemy, int playerDieRoll, int enemyDieRoll)
        {
            this.playerDieRoll = playerDieRoll;
            this.enemyDieRoll = enemyDieRoll;
            if (player.strength + playerDieRoll > enemy.strength + enemyDieRoll)
            {
                enemy.TakeDamage(player.damage);
                winner = player.name;
            }
            else if (player.strength + playerDieRoll < enemy.strength + enemyDieRoll)
            {
                player.TakeDamage(enemy.damage);
                winner = enemy.name;
            }
            else // Tie
            {
                tie = true;
            }


            this.player = (Character)player.Clone();
            this.enemy = (Character)enemy.Clone();
        }

        internal static Round CalculateOneRound(Character player, Character enemy)
        {
            Random rnd = new Random();
            return new Round(player, enemy, rnd.Next(1, 6), rnd.Next(1, 6));
        }

        internal Character getWinner()
        {
            if (player.name.Equals(winner))
            {
                return player;
            } else if (enemy.name.Equals(winner))
            {
                return enemy;
            } else
            {
                throw new Exception("The was no winner in this round. Check for tie!");
            }
        }
    }
}
