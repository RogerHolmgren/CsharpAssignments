using Lexicon.CSharp.InfoGenerator;
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
        private static NameGenerator randomNames = new NameGenerator();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8; // For the heart char to work.
            Character player = CreateCharacter();
            Console.Clear();
            PrintCharacterSheet(player, 0, 0);
            myBattles = new List<Battle>();

            // Main game loop
            Character nextEnemyEncounter = getRandomEnemy();
            while (!player.IsRetired && player.IsAlive())
            {
                // Generate and show next battle
                var newBattle = new Battle(player, nextEnemyEncounter);
                PrintBattleScreen(newBattle);

                // check for levelup
                if (nextEnemyEncounter.level % 3 == 0)
                    levelup(player);

                // Start the new battle or retire character
                PrintBattleScreen(newBattle);
                bool menu = true;
                while (menu)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.Q:
                            player.IsRetired = true;
                            menu = false;
                            break;
                        case ConsoleKey.Spacebar:
                            EnterBattle(newBattle);
                            nextEnemyEncounter = getRandomEnemy();
                            menu = false;
                            break;
                    }
                }
            }
            PrintBattleReport();
        }

        private static void levelup(Character player)
        {
            player.level++;
            popup(23, 1);
            bool levelupMenu = true;
            while (levelupMenu)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D1:
                        player.heal(99);
                        levelupMenu = false;
                        break;
                    case ConsoleKey.D2:
                        player.damage++;
                        levelupMenu = false;
                        break;
                    case ConsoleKey.D3:
                        player.strength++;
                        levelupMenu = false;
                        break;
                    case ConsoleKey.D4:
                        player.maxHealth++;
                        player.currentHealth++;
                        levelupMenu = false;
                        break;
                }
            }
        }

        private static Character getRandomEnemy()
        {
            int enemyLevel = myBattles.Count + 1;
            Character newEnemy = Character.GetEnemyCharacter(randomNames.GetRandomName(), enemyLevel);
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
            Console.WriteLine("--- Welcome to ArenaFighter! ---");
            Console.Write("\nName your character: ");
            var player = Character.GetPlayerCharacter(Console.ReadLine());
            do
            {
                RollStats(player);
                Console.Clear();
                Console.WriteLine("------ Character Creation ------");
                Console.Write("Review the stats for ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(player.name);
                Console.ResetColor();
                Console.WriteLine("--------------------------------");
                Console.WriteLine("> Health: " + player.maxHealth);
                Console.WriteLine("> Damage: " + player.damage);
                Console.WriteLine("> Attack: " + player.strength);
                Console.WriteLine("Total sum for your stats are: " + (player.maxHealth + player.damage + player.strength));
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Press 'Enter' if you are happy with your stats.");
                Console.WriteLine("Press 'Space' to roll for new stats.");
            } while (Console.ReadKey(true).Key != ConsoleKey.Enter);

            return player;
        }
        /// <summary>
        /// Rolled stats always have approximately the same sum. Higher level gives higher stats.
        /// </summary>
        private static void RollStats(Character character)
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
            Console.SetCursorPosition(34, 3);
            Console.Write("< VS >");
            Console.SetCursorPosition(0, 8);

            if (b.Rounds.Count > 0)
            {
                int roundsCount = 1;
                foreach (var r in b.Rounds)
                {
                    Console.Write($"[{roundsCount++}]------------------------\nBattle result: ");
                    Console.Write($"{r.player.name} {r.player.strength + r.playerDieRoll} ({r.player.strength} + {r.playerDieRoll})");
                    Console.WriteLine($" vs {b.enemy.name} {r.enemy.strength + r.enemyDieRoll} ({r.enemy.strength} + {r.enemyDieRoll})");
                    if (r.tie)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("There was a tie!");
                    }
                    else if (!r.player.IsAlive())
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"You were killed by {r.enemy.name}!");
                    }
                    else if (!r.enemy.IsAlive())
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{r.enemy.name} was slain");
                    }
                    else
                    {
                        if (r.player.Equals(r.getWinner()))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        Console.WriteLine($"{r.getWinner().name} won and did {r.getWinner().damage} damage.");
                    }
                    Console.ResetColor();
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
            WriteAt("+-------------------------------+", x, y + 6);

            for (int i = 1; i < 6; i++)
            {
                WriteAt("|", x + 32, y + i);
            }

            Console.ResetColor();
        }
        private static void PrintBattleReport()
        {
            Console.Clear();
            if (myBattles.Count > 0)
            {
                PrintCharacterSheet(myBattles[myBattles.Count - 1].player, 0, 0);
                Console.SetCursorPosition(0, 8);
                Console.WriteLine($"{myBattles.Last().player.name} played {myBattles.Count} battles!");
                int battleCount = 1;
                int totalDamage = 0;
                foreach (var battle in myBattles)
                {
                    Console.Write($"> Battle {battleCount++} had {battle.Rounds.Count} rounds. ");
                    Console.WriteLine(battle.Rounds.Last().getWinner().name + " won that battle.");
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
            } else
            {
                Console.WriteLine("You retired without having fought a single fight!");
            }
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
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            WriteAt("+-------[ Level Up ]----------+", x, y);
            WriteAt("| Choose one of the options!  |", x, y + 1);
            WriteAt("| 1. Heal upp to max          |", x, y + 2);
            WriteAt("| 2. +1 Damage                |", x, y + 3);
            WriteAt("| 3. +1 Strength              |", x, y + 4);
            WriteAt("| 4. Increase Max Health by 1 |", x, y + 5);
            WriteAt("+-----------------------------+", x, y + 6);
            Console.ResetColor();
        }
    }
}
