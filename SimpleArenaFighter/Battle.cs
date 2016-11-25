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
        /// This method saves a Round object to a list.
        /// </summary>
        /// <param name="round"></param>
        internal void SaveRoundToBattleLog(Round round)
        {
            rounds.Add(round);
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
                        Console.WriteLine($"{round.winner.characterName} won round {roundCount++}");
                    }
                }
            }
        }
    }
}