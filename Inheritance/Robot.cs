using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    class Robot : MechanicalThing, IMultiLocomotion
    {
        private string _state = "not set";

        public Robot(string Id, int CreationDate) : base(Id, CreationDate)
        {
        }

        public override void MakeASound()
        {
            Console.WriteLine("H311o");
        }

        public void Jump()
        {
            if (_state == "legs")
            {
                Console.WriteLine($"{Id} jumped 10cm.");
            }
            else
            {
                Console.WriteLine($"{Id} cannot jump in current state({_state}).");
            }

        }

        public void Move()
        {
            if (_state == "legs")
            {
                Console.WriteLine($"{Id} walked 100m.");
            }
            else if (_state == "serpent")
            {
                Console.WriteLine($"{Id} slithered 40m.");
            }
            else
            {
                Console.WriteLine($"{Id} cannot move in current state({_state}).");
            }
        }

        public void SetLocomotionState(string state)
        {
            this._state = state;
        }
    }
}
