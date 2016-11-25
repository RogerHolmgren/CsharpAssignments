using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleArenaFighter
{
    class Program
    {
        static Random numberGenerator = new Random();
        static List<Battle> myBattles = new List<Battle>();

        static void Main(string[] args)
        {
            Console.Write("Give your character a name: ");
            Character player = GenerateCharacterWithName(Console.ReadLine());

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
                Battle currentBattle = new Battle(player, GenerateEnemyCharacterWithRandomName());
                PrintBattlePreface(currentBattle);
                while (currentBattle.AllOpponentsAreAlive())
                {
                    ResolveOneRound(currentBattle);
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
        /// Creates a new round and compares the die rolls with stats from the opponents to
        /// find out who won and lost this round.
        /// </summary>
        /// <param name="b"></param>
        private static void ResolveOneRound(Battle b)
        {
            // 'b' is ok for variablename because:
            // - it's used often so shorter name makes the code cleaner
            // - it is only used inside this method
            // - the method only have one argument and it is clear that b means battle.
            Round thisRound = new Round(numberGenerator);
            int playerFightValue = b.player.strength + thisRound.playerRoll;
            int enemyFightValue = b.enemy.strength + thisRound.enemyRoll;

            if (playerFightValue > enemyFightValue) // Player Won :)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                thisRound.WinnerAttacksLoser(b.player, b.enemy); // Player hurts enemy
            }
            else if (playerFightValue < enemyFightValue) // Enemy won :(
            {
                Console.ForegroundColor = ConsoleColor.Red;
                thisRound.WinnerAttacksLoser(b.enemy, b.player); // Enemy hurts player
            }

            thisRound.PrintResults();
            Console.ResetColor();
            // Always save thisRound to the battle log of the current battle.
            b.SaveRoundToBattleLog(thisRound);
            Console.WriteLine("-- press any key to continue --");
            Console.ReadKey(true); // Wait for any key to start next round.
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

        /// <summary>
        /// This method creates a temporary character with a name.
        /// Adds random values to health, strength, and damage in the character object.
        /// Then it returns the object.
        /// </summary>
        /// <param name="characterName"></param>
        /// <returns></returns>
        private static Character GenerateCharacterWithName(string characterName)
        {
            Character temporaryCharacter = new Character(characterName);
            temporaryCharacter.health = numberGenerator.Next(8, 20);
            temporaryCharacter.strength = numberGenerator.Next(3, 8);
            temporaryCharacter.damage = numberGenerator.Next(3, 8);
            return temporaryCharacter;
        }

        /// <summary>
        /// Return a new enemy character by using GenerateCharacterWithName, but with
        /// an enemy name instead of playerName.
        /// </summary>
        /// <returns></returns>
        private static Character GenerateEnemyCharacterWithRandomName()
        {
            string randomEnemyName = "RandomEnemyName"; //TODO should be infoGenerator code...
            return GenerateCharacterWithName(randomEnemyName);
        }
    }
}
