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
            Console.OutputEncoding = Encoding.UTF8; // For the heart character to work.

            Character player = CreateCharacter();
            myBattles = new List<Battle>();
            while (player.IsAlive())
            {
                int enemyLevel = myBattles.Count + 1;
                Character newEnemy = Character.GetEnemyCharacter(enemyLevel);
                var newBattle = new Battle(player, newEnemy);
                EnterBattleMode(newBattle);
                
            }


        }

        private static void EnterBattleMode(Battle newBattle)
        {
            while (true)
            {

            }
            myBattles.Add(newBattle);
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
        /// Rolled stats always have approximatly the same sum. Higher level gives higher stats.
        /// </summary>
        public static void RollStats(Character character)
        {
            int minValue = 2 + character.level;
            int maxValue = character.isPlayer ? 12 + character.level : 17 + character.level; // adds 5 for player advantage.
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


        /*
        private static void BattleReport(Battle myBattle)
        {
            foreach (var round in myBattle.Rounds)
            {
                Console.WriteLine("Player rolled " + round.playerDiceRoll);
            }
        }

        private static void DoBattle(ref Battle myBattle)
        {
            bool continueBattle = true;
            while (continueBattle)
            {
                RedrawScreen(myBattle);

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Q:
                        continueBattle = false;
                        break;
                    case ConsoleKey.Spacebar:
                    case ConsoleKey.F10:
                        myBattle.Fight();
                        break;
                    default:
                        break;
                }

                if (myBattle.IsDone())
                {
                    RedrawScreen(myBattle);
                    continueBattle = false;
                }
            }
        }



        private static void PrintCharacterSheet(Character c, int x, int y)
        {
            WriteAt("+-------------------------------+", x, y);
            WriteAt("| ", x, y + 1);
            Console.ForegroundColor = c.isEnemy ? ConsoleColor.Red : ConsoleColor.Green;
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
        private static void RedrawScreen(Battle b)
        {
            Console.Clear();
            PrintCharacterSheet(b.player, 0, 0);
            if (b.enemy.IsAlive())
            {
                PrintCharacterSheet(b.enemy, 41, 0);
                Console.SetCursorPosition(34, 4);
                Console.Write("< VS >");
                Console.SetCursorPosition(0, 11);
                Console.WriteLine("A foe has appeared! 'space' to fight, 'Q' to quit.");
            }

            Console.SetCursorPosition(0, 12);

            if (b.Rounds.Count > 0)
            {
                Round r = b.Rounds[b.Rounds.Count - 1];
                if (r.tie)
                {
                    Console.WriteLine("There was a tie!");
                }
                else
                {
                    Console.Write($"Rolls: {b.player.name} {r.playerStrength + r.playerDiceRoll} ({r.playerStrength} + {r.playerDiceRoll})");
                    Console.WriteLine($" vs {b.enemy.name} {r.enemyStrength + r.enemyDiceRoll} ({r.enemyStrength} + {r.enemyDiceRoll})");
                    Console.WriteLine($"{r.winnerName} won and did {r.winnerDamage} damage.");
                }

            }


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

        private static void WriteAt(string s, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(s);
        }

        */
    }
}
