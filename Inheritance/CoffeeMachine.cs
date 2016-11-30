using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    class CoffeeMachine : MechanicalThing
    {
        public CoffeeMachine(string Id, int CreationDate) : base(Id, CreationDate)
        {
        }

        public override void MakeASound()
        {
            Console.WriteLine("Pfroo!");
        }
    }
}
