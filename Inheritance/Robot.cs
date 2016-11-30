using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    class Robot : MechanicalThing, ILegLocomotion
    {

        public Robot(string Id, int CreationDate) : base(Id, CreationDate)
        {
        }

        public override void MakeASound()
        {
            Console.WriteLine("H311o");
        }

        public void Jump()
        {
            Console.WriteLine($"{Id} jumped 10cm.");
        }

        public void Move()
        {
            Console.WriteLine($"{Id} walked 100m.");

        }
    }
}
