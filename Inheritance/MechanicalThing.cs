using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    abstract class MechanicalThing
    {
        public string Id { get; private set; }
        public int CreationDate { get; private set; }

        public MechanicalThing(string Id, int CreationDate)
        {
            this.Id = Id;
            this.CreationDate = CreationDate;
        }

        public abstract void MakeASound();
    }
}