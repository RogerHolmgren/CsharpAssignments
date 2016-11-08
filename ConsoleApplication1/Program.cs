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
            Console.OutputEncoding = Encoding.UTF8; // For the heart character to work.
            var player = CreateCharacter();
            var enemy = Character.GetEnemyCharacter(1);
            var myBattle = new Battle(player, enemy);
            startBattle(myBattle);



            //popup(12,8);
            //Console.SetCursorPosition(0, 20);
        }

        private static void startBattle(Battle myBattle)
        {
            PrintOpponents(myBattle);
            bool continueBattle = true;
            while (continueBattle)
            {
                Console.WriteLine("A foe has appeared! 'space' to fight, 'Q' to quit.");
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Q:
                        continueBattle = false;
                        break;
                    case ConsoleKey.Spacebar:
                        myBattle.Fight();
                        break;
                    default:
                        break;
                }

            }
        }

        private static Character CreateCharacter()
        {
            Console.Write("Name your character: ");
            var player = Character.GetPlayerCharacter(Console.ReadLine());
            do
            {
                player.rollStats();
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

        private static void PrintOpponents(Battle b)
        {
            Console.Clear();
            b.player.printCharacterSheet(0, 0);
            b.enemy.printCharacterSheet(41, 0);
            Console.SetCursorPosition(34, 4);
            Console.Write("< VS >");
            Console.SetCursorPosition(0, 11);
        }
    }
}
