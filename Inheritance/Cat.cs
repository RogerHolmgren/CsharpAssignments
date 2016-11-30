using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    class Cat : LivingThing, ILegLocomotion
    {
        public Cat(string Name, int Age) : base(Name, Age)
        {
        }

        public override void Speak()
        {
            Console.WriteLine("Mjau");
        }


        public void Jump()
        {
            Console.WriteLine($"{Name} jumped 2m.");
        }

        public void Move()
        {
            Console.WriteLine($"{Name} walked 40m.");
        }
    }
}
