using System;
using System.Collections.Generic;

namespace SimpleArenaFighter
{
    internal class Battle
    {
        public Character player { get; private set; }
        public Character enemy { get; private set; }
        public List<Round> rounds { get; private set; }

        /// <summary>
        /// When instantiated a new Battle must have a player and a enemy character.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="enemy"></param>
        public Battle(Character player, Character enemy)
        {
            rounds = new List<Round>();
            this.player = player;
            this.enemy = enemy;
        }

        /// <summary>
        /// Returns true when the health is above zero for both player and enemy.
        /// </summary>
        /// <returns></returns>
        internal bool AllOpponentsAreAlive()
        {
            return player.health > 0 && enemy.health > 0;
        }

        /// <summary>
        /// Creates a new round and returns it.
        /// </summary>
        /// <param name="b"></param>
        internal Round ResolveOneRound()
        {
            rounds.Add(new Round(player, enemy)); // Save round to the battle log.
            return rounds[rounds.Count -1]; // Return the last element in the list.
        }

        /// <summary>
        /// This is a static method inside the battle class. It takes a list of battles
        /// and prints a report for each battle and for each round in that battle.
        /// </summary>
        public static void PrintReport(List<Battle> myBattles)
        {
            int battleCount = 1;
            foreach (var battle in myBattles)
            {
                Console.WriteLine($"Battle: {battleCount++}");
                int roundCount = 1;
                foreach (var round in battle.rounds)
                {
                    if (round.WasATie())
                    {
                        Console.WriteLine($"There was a tie. No one won round {roundCount++}.");
                    }
                    else
                    {
                        Console.ForegroundColor = battle.player.Equals(round.winner) ? ConsoleColor.Green : ConsoleColor.Red;
                        Console.WriteLine($"{round.winner.characterName} won round {roundCount++}");
                        Console.ResetColor();
                    }
                }
            }
        }
    }
}