using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class Drink : Product
    {
        public Drink(string name, int price, string description) : base(name, price, description)
        {
        }

        public override void Use()
        {
            Console.Write("You drank the " + name);
        }
    }
}
