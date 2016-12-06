using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class Program
    {
        static VendingMachine vm = new VendingMachine(@"..\..\products.json");

        static void Main(string[] args)
        {

            bool quit = false;
            PrintUI();
            do
            {
                Console.Write(": ");
                args = Console.ReadLine().ToLower().Trim().Split();
                try
                {
                    switch (args[0])
                    {
                        case "add":
                        case "a":
                            int totalMoneyAdded = vm.AddMoney(args);
                            Console.WriteLine($"A sum of {totalMoneyAdded} will be added to your total Balance.");
                            break;
                        case "buy":
                        case "b":
                            Product p = vm.BuyProductWithKey(args[1]);
                            Console.WriteLine($"You bought {p.name}");
                            break;
                        case "info":
                        case "i":
                            Console.WriteLine(vm.GetInfo(args));
                            break;
                        case "quit":
                        case "q":
                            Console.WriteLine("Thanks for buying, calculating change.");
                            quit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid arguments");
                            break;
                    }
                }
                catch (VendingMachine.VendingMachineException e)
                {
                    Console.WriteLine(e.Message);
                }

                Console.WriteLine("Press any key to continue.");
                Console.ReadKey(true);
                PrintUI();
            } while (!quit);

            PrintChange();
        }

        static void PrintChange()
        {
            Console.Clear();
            Console.WriteLine("Your change is as follows:");
            Dictionary<string, int> change = vm.GetChange();
            if (change.Count() == 0)
            {
                Console.WriteLine("No change.");
            }
            else
            {
                foreach (var coin in change)
                {
                    Console.WriteLine($"{coin.Value}x{coin.Key}");
                }
            }
        }

        static void PrintUI()
        {
            Console.Clear();
            Console.WriteLine("### Vending Machine Menu ###");
            Console.WriteLine("- add [1kr,2kr,5kr,10kr,20kr,50kr,100kr,500kr .. ] - to add money.");
            Console.WriteLine("- info [code .. ] ie: \"info A1\" - to view product info.");
            Console.WriteLine("- buy [code] ie: \"buy A1\" - to buy a product.");
            Console.WriteLine("- quit or q - to quit.");
            Console.WriteLine();
            PrintSelectionGrid();
            Console.WriteLine();
            Console.WriteLine($"Balance: {vm.Balance}kr.");
        }

        static void PrintSelectionGrid()
        {
            string RowColumnKey = vm.GetSelection().First().Key;
            foreach (var prod in vm.GetSelection())
            {
                if (!RowColumnKey.StartsWith(prod.Key[0] + ""))
                {
                    Console.WriteLine();
                }
                Console.Write(RowColumnKey = prod.Key + ": " + prod.Value.name.PadRight(vm.LongestProductName.Length + 1));
            }
            Console.WriteLine();
        }
    }
}
