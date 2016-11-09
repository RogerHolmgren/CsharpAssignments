using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaFighter
{
    class Character
    {
        private static readonly int DEFAULT_LEVEL = 1;
        private static readonly char HEART = '\u2665';

        private int Armor = 0;
        private int DieSizeModifier = 0;
        private int DieResultModifier = 0;

        public string name { get; private set; }
        public int maxHealth { get; private set; }
        public int currentHealth { get; private set; }
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

        internal int RollDie()
        {
            Random rnd = new Random(maxHealth * damage);
            return rnd.Next(1, 6 + DieSizeModifier) + DieResultModifier;
        }

        public static Character GetEnemyCharacter(int level)
        {
            var enemy = new Character("Grognak");
            enemy.isEnemy = true;
            enemy.level = level;
            enemy.rollStats();
            return enemy;
        }

        internal bool IsAlive()
        {
            return currentHealth != 0;
        }

        public void TakeDamage(int damage)
        {
            if (damage >= Armor)
            {
                currentHealth -= (damage - Armor);
            }
            currentHealth = currentHealth < 0 ? 0 : currentHealth; // currentHealth cannot be below zero.
        }

        /// <summary>
        /// Cannot invoke Character Class directly. Class instantiation needs to be done through GetPlayerCharacter or GetEnemyCharacter.
        /// </summary>
        /// <param name="name"></param>
        private Character(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Rolled stats always have approximatly the same sum. Higher level gives higher stats.
        /// </summary>
        public void rollStats()
        {
            int levelMultiplier = 12 + level;
            levelMultiplier += isEnemy ? 0 : 5; // adds 5 for player advantage.
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
    }
}
