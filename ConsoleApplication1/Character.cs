using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaFighter
{
    class Character
    {
        private static readonly int DEFAULT_LEVEL = 5;
        private static readonly char HEART = '\u2665';

        public string name { get; private set; }
        public int currentHealth { get; private set; }
        public int maxHealth { get; private set; }
        public int damage { get; private set; }
        public int strength { get; private set; }
        public int level { get; private set; }
        public bool isEnemy { get; private set; }

        public static Character GetPlayerCharacter(string name)
        {
            var player = new Character(name == "" ? "Noname" : name);
            player.isEnemy = false;
            player.level = DEFAULT_LEVEL;
            player.rollStats();
            return player;
        }
        public static Character GetEnemyCharacter(int level)
        {
            var enemy = new Character("Grognak");
            enemy.isEnemy = true;
            enemy.level = level;
            enemy.rollStats();
            return enemy;
        }

        private Character(string name)
        {
            this.name = name;
        }

        public void rollStats()
        {
            int levelMultiplier = 12 + level;
            Random rnd = new Random();
            double a = rnd.Next(3, levelMultiplier);
            double b = rnd.Next(3, levelMultiplier);
            double c = rnd.Next(3, levelMultiplier);
            double k = (a + b + c) / levelMultiplier;
            a /= k;
            b /= k;
            c /= k;

            currentHealth = maxHealth = (int)(a + 0.5) + level;
            damage = (int)(b + 0.5);
            strength = (int)(c + 0.5);
        }

        public string currentHealthAsHeartbar()
        {
            string bar = "";
            for (int i = 0; i < currentHealth; i++)
            {
                bar += HEART;
            }
            return bar;
        }
        public string lostHealthAsHeartbar()
        {
            string bar = "";
            for (int i = 0; i < maxHealth - currentHealth; i++)
            {
                bar += HEART;
            }
            return bar;
        }

        public void printCharacterSheet(int x, int y)
        {
            WriteAt("+-------------------------------+", x, y);
            WriteAt("| ", x, y + 1);
            Console.ForegroundColor = isEnemy ? ConsoleColor.Red : ConsoleColor.Green;
            Console.Write(name);
            WriteAt($"lvl: {level}", x + 24, y + 1);
            Console.ForegroundColor = ConsoleColor.Gray;
            WriteAt("| -----------------------------", x, y + 2);
            WriteAt("| Health: ", x, y + 3);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(currentHealthAsHeartbar());
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(lostHealthAsHeartbar());
            Console.ForegroundColor = ConsoleColor.Gray;
            WriteAt("| Damage: " + damage, x, y + 4);
            WriteAt("| Attack: " + strength, x, y + 5);
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
    }
}
