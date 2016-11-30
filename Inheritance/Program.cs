using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    class Program
    {
        static List<Object> list = new List<Object> {
            new Human("Roger", 33),
            new Cat("Misse", 3),
            new Snake("Snape", 2),
            new Robot("Data", 2338),
            new CoffeeMachine("Nespresso2.0", 2015)
        };

        static void Main(string[] args)
        {
            Console.WriteLine("--- Perform all actions a LivingThing can do:");
            foreach (var item in list)
            {
                try
                {
                    var living = (LivingThing)item;
                    Console.Write($"LivingThing: {living.Name}({living.Age}) says, ");
                    living.Speak();
                }
                catch (Exception)
                {
                    Console.WriteLine(item.GetType().Name + " is not a living thing!");
                }
            }

            Console.WriteLine("\n--- Perform all actions a MechanicalThing can do:");
            foreach (var item in list)
            {
                try
                {
                    var mechanical = (MechanicalThing)item;
                    Console.Write($"MechanicalThing: {mechanical.Id}({mechanical.CreationDate}) sounds like, ");
                    mechanical.MakeASound();
                }
                catch (Exception)
                {
                    Console.WriteLine(item.GetType().Name + " is not a mechanical thing!");
                }
            }

            Console.WriteLine("\n--- Perform all actions an object with ILegLocomotion can do:");
            foreach (var item in list)
            {
                try
                {
                    var hasLegs = (ILegLocomotion)item;
                    hasLegs.Move();
                    hasLegs.Jump();
                }
                catch (Exception)
                {
                    Console.WriteLine(item.GetType().Name + " cannot walk or jump!");
                }
            }

            Console.WriteLine("\n--- Perform all actions an object with ISerpentineLocomotion can do:");
            foreach (var item in list)
            {
                try
                {
                    var slither = (ISerpentineLocomotion)item;
                    slither.Move();
                }
                catch (Exception)
                {
                    Console.WriteLine(item.GetType().Name + " cannot slither!");
                }
            }

        }
    }
}
