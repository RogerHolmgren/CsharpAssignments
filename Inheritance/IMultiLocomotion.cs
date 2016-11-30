using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    interface IMultiLocomotion : ILegLocomotion, ISerpentineLocomotion
    {
        void SetLocomotionState(string state);
    }
}
