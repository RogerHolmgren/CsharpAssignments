using System;

namespace SimpleArenaFighter
{
    internal class Round
    {
        private static Random _numberGenerator = new Random();

        public Character winner { get; private set; }
        public Character loser { get; private set; }
        public int winnerFightValue { get; private set; }
        public int loserfightValue { get; private set; }

        /// <summary>
        /// When instanciating a new Round fightValues, winner and loser is automatically resolved.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="enemy"></param>
        public Round(Character player, Character enemy)
        {
            int playerFightValue = player.strength + player.DieRoll();
            int enemyFightValue = enemy.strength + enemy.DieRoll();

            // Determine who won
            if (playerFightValue > enemyFightValue) // Player Won :)
            {
                this.winner = player;
                this.loser = enemy;
                this.winnerFightValue = playerFightValue;
                this.loserfightValue = enemyFightValue;
            }
            else if (playerFightValue < enemyFightValue) // Enemy won :(
            {
                this.winner = enemy;
                this.loser = player;
                this.winnerFightValue = enemyFightValue;
                this.loserfightValue = playerFightValue;
            }
            if (!WasATie())
            {
                loser.health -= winner.damage;
            }
        }

        internal void PrintResults()
        {
            if (WasATie())
            {
                Console.WriteLine("There was a tie, the battle moves on to the next round!");
            }
            else
            {
                Console.WriteLine($"{winner.characterName} won this round. {loser.characterName} took {winner.damage} damage and has {loser.health} health left.");
            }
        }

        internal bool WasATie()
        {
            return winner == null;
        }
    }
}