using System;

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