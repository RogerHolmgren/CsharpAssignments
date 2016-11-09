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

        public void Fight()
        {
            var round = new Round();
            round.playerStrength = player.strength;
            round.playerDiceRoll = player.RollDie();
            round.enemyStrength = enemy.strength;
            round.enemyDiceRoll = enemy.RollDie();

            if (round.playerStrength + round.playerDiceRoll > round.enemyStrength + round.enemyDiceRoll)
            {
                enemy.TakeDamage(player.damage);
                round.winnerName = player.name;
                round.winnerDamage = player.damage;
                round.loserName = enemy.name;
                round.loserHealthAfter = enemy.currentHealth;
            }
            else if (round.playerStrength + round.playerDiceRoll < round.enemyStrength + round.enemyDiceRoll)
            {
                player.TakeDamage(enemy.damage);
                round.winnerName = enemy.name;
                round.winnerDamage = enemy.damage;
                round.loserName = player.name;
                round.loserHealthAfter = player.currentHealth;
            }
            else
            {
                round.tie = true;
            }
            Rounds.Add(round);
        }

        /// <summary>
        /// If either player or enemy is not alive, battle is done!
        /// </summary>
        /// <returns></returns>
        internal bool IsDone()
        {
            return !player.IsAlive() || !enemy.IsAlive();
        }
    }
}
