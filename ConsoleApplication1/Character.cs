using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaFighter
{
    class Character
    {
        private readonly int DEFAULT_LEVEL = 5;
        private readonly char HEART = '\u2665';

        internal string name { get; private set; }
        internal int currentHealth { get; private set; }
        internal int maxHealth { get; private set; }
        internal int strength { get; private set; }
        internal int attack { get; private set; }
        internal int level { get; private set; }

        public Character(string name)
        {
            this.name = name;
            this.level = DEFAULT_LEVEL;
            rollStats();
        }

        public Character(string name, int level) : this(name)
        {
            this.name = name;
            this.level = level;
            rollStats();
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
            strength = (int)(b + 0.5);
            attack = (int)(c + 0.5);
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
            WriteAt("| Character name: " + name, x, y + 1);
            WriteAt("| -----------------------------", x, y + 2);
            WriteAt("| Health: ", x, y + 3);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(currentHealthAsHeartbar());
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(lostHealthAsHeartbar());
            Console.ForegroundColor = ConsoleColor.Gray;
            WriteAt("| Power: " + strength, x, y + 4);
            WriteAt("| Attack: " + attack, x, y + 5);
            WriteAt("| -----------------------------", x, y + 6);
            WriteAt("| Items:", x, y + 7);
            WriteAt("| 1: +1 Sword", x, y + 8);
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
