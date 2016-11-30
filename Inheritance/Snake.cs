using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    class Snake : LivingThing, ISerpentineLocomotion
    {
        public Snake(string Name, int Age) : base(Name, Age)
        {
        }

        public void Move()
        {
            Console.WriteLine($"{Name} slithered 50m");
        }

        public override void Speak()
        {
            Console.WriteLine("Hsss");
        }
    }
}
