using System;

namespace SimpleArenaFighter
{
    internal class Round
    {
        internal int playerRoll { get; private set; }
        internal int enemyRoll { get; private set; }
        public Character winner { get; private set; }
        public Character loser { get; private set; }

        /// <summary>
        /// When a round is instanciated playerRoll and enemyRoll get random values.
        /// </summary>
        /// <param name="numberGenerator"></param>
        public Round(Random numberGenerator)
        {
            this.playerRoll = numberGenerator.Next(1, 6);
            this.enemyRoll = numberGenerator.Next(1, 6);
        }

        /// <summary>
        /// the winner deals damage to the loser
        /// </summary>
        /// <param name="player"></param>
        /// <param name="enemy"></param>
        internal void WinnerAttacksLoser(Character winner, Character loser)
        {
            this.winner = winner;
            this.loser = loser;
            loser.health -= winner.damage;
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