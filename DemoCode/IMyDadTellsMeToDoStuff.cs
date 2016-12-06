using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCode
{
    interface IMyDadTellsMeToDoStuff 
    {
        void BuyGroceries(List<string> shoppingList);
        bool DeliverLetter(string letter);
    }
}
