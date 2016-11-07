using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaFighter
{
    class Battle
    {
        private Character player;
        private Character enemy;

        public Battle(Character player, Character enemy)
        {
            this.player = player;
            this.enemy = enemy;
        }

        public void PrintOpponents()
        {
            Console.Clear();
            player.printCharacterSheet(0,0);
            enemy.printCharacterSheet(41,0);
            Console.SetCursorPosition(34, 4);
            Console.Write("< VS >");
            Console.SetCursorPosition(0, 20);
        }


    }
}
