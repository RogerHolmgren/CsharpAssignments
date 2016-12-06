using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCode
{
    class HumanChild : IMyDadTellsMeToDoStuff // But don't care how I do it
    {
        public void BuyGroceries(List<string> shoppingList)
        {
            shoppingList.Add("Lots of candy");
            Console.WriteLine("Buy all things on the list.");
        }

        public bool DeliverLetter(string letter)
        {
            Console.WriteLine("Throw away letter");
            return true; // Say that you delivered the letter anyway
        }

        public void playWithFriends()
        {
            // 
        }
    }
}
