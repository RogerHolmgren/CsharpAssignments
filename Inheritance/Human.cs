using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    class Human : LivingThing, ILegLocomotion
    {
        public Human(string Name, int Age) : base(Name, Age)
        {
        }

        public override void Speak()
        {
            Console.WriteLine("Hello");
        }

        public void Jump()
        {
            Console.WriteLine($"{Name} jumped 50cm.");
        }

        public void Move()
        {
            Console.WriteLine($"{Name} walked 25m.");
        }
    }
}
