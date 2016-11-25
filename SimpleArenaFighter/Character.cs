using System;

namespace SimpleArenaFighter
{
    internal class Character
    {
        internal string characterName { get; private set; }
        internal int damage { get; set; }
        internal int health { get; set; }
        internal int strength { get; set; }

        /// <summary>
        /// Creates a new character with a fixed name. The name cannot be changed.
        /// If you want to change name you need to create a new character.
        /// </summary>
        /// <param name="playerName"></param>
        public Character(string playerName)
        {
            this.characterName = playerName;
        }

        /// <summary>
        /// Prints the name and stats to console.
        /// </summary>
        public void printStats()
        {
            Console.WriteLine(characterName);
            Console.WriteLine($"Health: {health}");
            Console.WriteLine($"Strength: {strength}");
            Console.WriteLine($"Damage: {damage}");
        }

        /// <summary>
        /// If health is zero or below return true.
        /// </summary>
        /// <returns></returns>
        internal bool IsDead()
        {
            return health <= 0;
        }
    }
}