using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleArenaFighter
{
    class Program
    {
        static List<Battle> myBattles = new List<Battle>();

        static void Main(string[] args)
        {
            Console.Write("Give your character a name: ");
            Character player = new Character(Console.ReadLine());

            EnterBattleArenaGameLoop(player);

            Battle.PrintReport(myBattles);

            int finalScore = myBattles.Count + (player.IsDead() ? 0 : 5);
            Console.WriteLine($"Your score was: {finalScore}");
        }

        /// <summary>
        /// As long as player is alive and choose to continue battle, a new battle will be created.
        /// </summary>
        /// <param name="player"></param>
        private static void EnterBattleArenaGameLoop(Character player)
        {
            do
            {
                Battle currentBattle = new Battle(player, new Character("RandomEnemyName"));
                PrintBattlePreface(currentBattle);
                while (currentBattle.AllOpponentsAreAlive())
                {
                    Round thisRound = currentBattle.ResolveOneRound();
                    if (!thisRound.WasATie())
                    {
                        Console.ForegroundColor = player.Equals(thisRound.winner) ? ConsoleColor.Green : ConsoleColor.Red;
                    }
                    thisRound.PrintResults();
                    Console.ResetColor();
                    Console.WriteLine("-- press any key to continue --");
                    Console.ReadKey(true); // Wait for any key to start next round.
                }
                // When all rounds have been resolved add currentBattle to myBattles before while-loop restarts.
                myBattles.Add(currentBattle);
            } while (EndOfBattleMenu(player) == ConsoleKey.B);  // If menu return B continue battle, else quit BattleArena.
        }

        /// <summary>
        /// Prints player and enemy stats as a battle starts.
        /// </summary>
        /// <param name="currentBattle"></param>
        private static void PrintBattlePreface(Battle currentBattle)
        {
            Console.Clear();
            Console.WriteLine("You are in a battle.");
            currentBattle.player.printStats();
            Console.WriteLine("-----------------");
            currentBattle.enemy.printStats();
            Console.WriteLine("-----------------");
        }

        /// <summary>
        /// If player is alive the options to continue battle or retire is avalible, if dead the battle is over.
        /// </summary>
        /// <param name="player"></param>
        /// <returns>ConsoleKey</returns>
        private static ConsoleKey EndOfBattleMenu(Character player)
        {
            Console.Clear();
            Console.WriteLine("The battle is over!");
            player.printStats();
            if (player.IsDead())
            {
                Console.WriteLine("You died! Press any key to continue");
                Console.ReadKey(true);
                return ConsoleKey.Q;
            }
            else
            {
                Console.WriteLine("You are still alive. Do (B)attle again or (R)etire?");
                return Console.ReadKey(true).Key;
            }
        }
    }
}
