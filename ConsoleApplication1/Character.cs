using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaFighter
{
    public class Character
    {
        private static readonly int DEFAULT_LEVEL = 1;
        private static readonly char HEART = '\u2665';

        private int Armor = 0;
        private int DieSizeModifier = 0;
        private int DieResultModifier = 0;

        public string name { get; private set; }
        public bool isPlayer { get; private set; }
        public int maxHealth { get; set; }
        public int currentHealth { get; set; }
        public int damage { get; set; }
        public int strength { get; set; }
        public int level { get; set; }
        

        /// <summary>
        /// Cannot invoke Character Class directly. Class instantiation needs to be done through GetPlayerCharacter or GetEnemyCharacter.
        /// </summary>
        /// <param name="name"></param>
        private Character(string name)
        {
            this.name = name;
        }

        public static Character GetPlayerCharacter(string name)
        {
            var player = new Character(name == "" ? "Nameless" : name);
            player.isPlayer = true;
            player.level = DEFAULT_LEVEL;
            return player;
        }

        public static Character GetEnemyCharacter(int level)
        {
            var enemy = new Character("Grognak");
            enemy.isPlayer = false;
            enemy.level = level;
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
