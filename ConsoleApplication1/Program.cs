using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaFighter
{
    class Program
    {
        static void Main(string[] args)
        {
            // For the heart character to work.
            Console.OutputEncoding = Encoding.UTF8;
            var player = CreateCharacter();

            Console.Clear();
            player.printCharacterSheet(0,0);

            startBattle(player);



            //popup(12,8);
            //Console.SetCursorPosition(0, 20);
        }

        private static void startBattle(Character player)
        {
            var myBattle = new Battle(player, new Character("Enemy", 1));

                myBattle.PrintOpponents();


        }

        private static Character CreateCharacter()
        {
            Console.Write("Name your character: ");
            var player = new Character(Console.ReadLine());
            ConsoleKeyInfo key;
            do
            {
                Console.Clear();
                Console.WriteLine("Name: " + player.name);
                Console.WriteLine("Health: " + player.maxHealth);
                Console.WriteLine("Strength: " + player.strength);
                Console.WriteLine("Attack: " + player.attack);
                Console.WriteLine("Sum: " + (player.maxHealth + player.strength + player.attack));
                Console.WriteLine("Press Enter to accept, Space to reroll");
                key = Console.ReadKey(true);
                player.rollStats();
            } while (key.Key != ConsoleKey.Enter);


            return player;
        }
    }
}
