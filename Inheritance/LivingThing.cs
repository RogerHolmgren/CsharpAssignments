using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    abstract class LivingThing
    {
        public string Name { get; private set; }
        public int Age { get; private set; }

        public LivingThing(string Name, int Age)
        {
            this.Name = Name;
            this.Age = Age;
        }

        public abstract void Speak();
    }
}