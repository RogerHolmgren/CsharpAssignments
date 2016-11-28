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
        static void Main(string[] args)
        {
            VendingMachine vm = new VendingMachine(@"..\..\products.json");
            foreach (var item in vm.GetSelection())
            {
                Console.WriteLine(item.Key + ": "+ item.Value.name);
            }

        }
    }
}
