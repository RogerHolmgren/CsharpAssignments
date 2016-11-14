using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class Food : Product
    {
        public Food(string name, int price, string description) : base(name, price, description)
        {
        }

        public override void Use()
        {
            Console.Write("You ate the " + name);
        }
    }
}
