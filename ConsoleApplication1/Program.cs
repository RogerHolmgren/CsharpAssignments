using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaFighter
{
    class Program
    {
        private static List<Battle> myBattles;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8; // For the heart char to work.

            Character player = CreateCharacter();
            Console.Clear();
            PrintCharacterSheet(player, 0, 0);
            myBattles = new List<Battle>();

            // Main game loop
            while (!player.IsRetired && player.IsAlive())
            {
                var newBattle = new Battle(player, getRandomEnemy());
                PrintBattleScreen(newBattle);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Q:
                        player.IsRetired = true;
                        break;
                    case ConsoleKey.Spacebar:
                        EnterBattle(newBattle);
                        break;
                }
            }
            PrintBattleReport();
        }

        private static Character getRandomEnemy()
        {
            int enemyLevel = myBattles.Count + 1;
            Character newEnemy = Character.GetEnemyCharacter(enemyLevel);
            RollStats(newEnemy);
            return newEnemy;
        }

        private static void EnterBattle(Battle battle)
        {
            do
            {
                battle.saveToLog(Round.CalculateOneRound(battle.player, battle.enemy));
                PrintBattleScreen(battle);
                Console.WriteLine("\n--- Press space to continue! ---");
                Console.ReadKey(true);
            } while (!battle.IsDone());
            myBattles.Add(battle);
        }

        private static Character CreateCharacter()
        {
            Console.Write("Name your character: ");
            var player = Character.GetPlayerCharacter(Console.ReadLine());
            do
            {
                RollStats(player);
                Console.Clear();
                Console.WriteLine("Name: " + player.name);
                Console.WriteLine("Health: " + player.maxHealth);
                Console.WriteLine("Damage: " + player.damage);
                Console.WriteLine("Attack: " + player.strength);
                Console.WriteLine("Sum: " + (player.maxHealth + player.damage + player.strength));
                Console.WriteLine("Press Enter to accept, Space to reroll");
            } while (Console.ReadKey(true).Key != ConsoleKey.Enter);

            return player;
        }
        /// <summary>
        /// Rolled stats always have approximately the same sum. Higher level gives higher stats.
        /// </summary>
        public static void RollStats(Character character)
        {
            int minValue = 2 + character.level;
            int baseMaxValue = character.isPlayer ? 17 : 12; // Adds 5 for player advantage.
            int maxValue = baseMaxValue + character.level;
            Random rnd = new Random();
            double a = rnd.Next(minValue, maxValue);
            double b = rnd.Next(minValue, maxValue);
            double c = rnd.Next(minValue, maxValue);
            double k = (a + b + c) / maxValue;
            a /= k;
            b /= k;
            c /= k;

            character.currentHealth = character.maxHealth = (int)(a + 0.5) + character.level;
            character.damage = (int)(b + 0.5);
            character.strength = (int)(c + 0.5);
        }

        //---------------------------------------------------------------------
        //
        //      Functions that prints stuff to console. 
        //
        //---------------------------------------------------------------------

        private static void PrintBattleScreen(Battle b)
        {
            Console.Clear();
            PrintCharacterSheet(b.player, 0, 0);

            PrintCharacterSheet(b.enemy, 41, 0);
            Console.SetCursorPosition(34, 4);
            Console.Write("< VS >");
            Console.SetCursorPosition(0, 11);

            if (b.Rounds.Count > 0)
            {
                int roundsCount = 1;
                foreach (var r in b.Rounds)
                {
                    Console.Write($"[{roundsCount++}]------------------------\nBattle result: ");
                    if (r.tie)
                    {
                        Console.WriteLine("There was a tie!");
                    }
                    else if (!r.player.IsAlive())
                    {
                        Console.WriteLine("Player died");
                    }
                    else if (!r.enemy.IsAlive())
                    {
                        Console.WriteLine("Enemy died");
                    }
                    else
                    {
                        Console.Write($"{r.player.name} {r.player.strength + r.playerDieRoll} ({r.player.strength} + {r.playerDieRoll})");
                        Console.WriteLine($" vs {b.enemy.name} {r.enemy.strength + r.enemyDieRoll} ({r.enemy.strength} + {r.enemyDieRoll})");
                        Console.WriteLine($"{r.getWinner().name} won and did {r.getWinner().damage} damage.");
                    }
                }

            }
            else
            {
                Console.WriteLine("A foe has appeared! 'space' to fight, 'Q' to quit.");
            }
        }
        private static void PrintCharacterSheet(Character c, int x, int y)
        {
            WriteAt("+-------------------------------+", x, y);
            WriteAt("| ", x, y + 1);
            Console.ForegroundColor = c.isPlayer ? ConsoleColor.Green : ConsoleColor.Red;
            Console.Write(c.name);
            WriteAt($"lvl: {c.level}", x + 24, y + 1);
            Console.ForegroundColor = ConsoleColor.Gray;
            WriteAt("| -----------------------------", x, y + 2);
            WriteAt("| Health: ", x, y + 3);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(c.currentHealthAsHeartbar());
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(c.lostHealthAsHeartbar());
            Console.ForegroundColor = ConsoleColor.Gray;
            WriteAt("| Damage: " + c.damage, x, y + 4);
            WriteAt("| Strength: " + c.strength, x, y + 5);
            WriteAt("| -----------------------------", x, y + 6);
            WriteAt("| Items:", x, y + 7);
            WriteAt("| 1: NA", x, y + 8);
            WriteAt("+-------------------------------+", x, y + 9);

            for (int i = 1; i < 9; i++)
            {
                WriteAt("|", x + 32, y + i);
            }

            Console.ResetColor();
        }
        private static void PrintBattleReport()
        {
            Console.Clear();
            PrintCharacterSheet(myBattles[myBattles.Count - 1].player, 0, 0);
            Console.SetCursorPosition(0, 12);
            Console.WriteLine($"Player played {myBattles.Count} battles!");
            int battleCount = 1;
            int totalDamage = 0;
            foreach (var battle in myBattles)
            {
                Console.WriteLine($"> Battle {battleCount++} had {battle.Rounds.Count} rounds.");
                foreach (var round in battle.Rounds)
                {
                    if (!round.tie)
                    {
                        totalDamage += round.getWinner().Equals(round.player) ? round.player.damage : 0;
                    }

                }
            }
            Console.WriteLine($"Player retired: {myBattles.Last<Battle>().player.IsRetired}");
            Console.WriteLine($"Total damage done was {totalDamage}");
        }
        private static void WriteAt(string s, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(s);
        }
        private static void popup(int x, int y)
        {
            //origRow = Console.CursorTop;
            //origCol = Console.CursorLeft;

            // Draw the left side of a 5x5 rectangle, from top to bottom.
            WriteAt("+-------[ Shield ]---------+", x, y);
            WriteAt("| Removes the first damage |", x, y + 1);
            WriteAt("| taken each round.        |", x, y + 2);
            WriteAt("+--------------------------+", x, y + 3);
        }
    }
}
