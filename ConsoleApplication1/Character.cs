using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaFighter
{
    public class Character : ICloneable
    {
        private static readonly int DEFAULT_LEVEL = 1;
        private static readonly char HEART = '\u2665';

        public string name { get; private set; }
        public bool isPlayer { get; private set; }
        public int maxHealth { get; set; }
        public int currentHealth { get; set; }
        public int damage { get; set; }
        public int strength { get; set; }
        public int level { get; set; } = DEFAULT_LEVEL;
        public bool IsRetired { get; set; } = false;


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
            currentHealth = (currentHealth - damage < 0) ? 0 : currentHealth - damage; // currentHealth cannot be below zero.
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

        public object Clone()
        {
            Character copy = new Character(name);
            copy.isPlayer = isPlayer;
            copy.maxHealth = maxHealth;
            copy.currentHealth = currentHealth;
            copy.damage = damage;
            copy.strength = strength;
            copy.level = level;
            copy.IsRetired = IsRetired;
            return copy;
        }
    }
}
