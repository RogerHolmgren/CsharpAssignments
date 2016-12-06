using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace VendingMachine
{
    public class VendingMachine
    {
        private Dictionary<string, Product> _selection = new Dictionary<string, Product>();
        private int[] _allowed = { 1, 2, 5, 10, 20, 50, 100, 200, 500 };
        public int Balance { get; private set; }
        public string LongestProductName { get; private set; }

        public VendingMachine(List<Product> products)
        {
            PopulateSelection(products);
        }

        public VendingMachine(string jsonURL)
        {
            JsonSerializerSettings jsonSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects };
            string jsonContent = File.ReadAllText(jsonURL);
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(jsonContent, jsonSettings);

            PopulateSelection(products);
        }

        /// <summary>
        /// Create a dictionary with generated keys that follow this pattern:
        ///  A1 A2 A3
        ///  B1 B2 B3
        ///  etc...
        /// </summary>
        /// <param name="products"></param>
        private void PopulateSelection(List<Product> products)
        {
            products.Sort((p1, p2) => p1.name.Length.CompareTo(p2.name.Length));
            //var shortest = products.FirstOrDefault();
            LongestProductName = products.LastOrDefault().name;
            products.Sort((p1, p2) => p1.GetType().ToString().CompareTo(p2.GetType().ToString()));

            char row = 'A';
            int col = 1;
            foreach (var item in products)
            {
                _selection.Add($"{row}{col++}", item);
                if (col > 3)
                {
                    row++;
                    col = 1;
                }
            }
        }

        internal void PrintSelectionGrid()
        {
            string currentRow = _selection.First().Key;
            foreach (var prod in _selection)
            {
                if (!currentRow.StartsWith(prod.Key[0] + ""))
                {
                    Console.WriteLine();
                }
                Console.Write(currentRow = prod.Key + ": " + prod.Value.name.PadRight(LongestProductName.Length + 1));
            }
            Console.WriteLine();
        }

        internal Dictionary<string, Product> GetSelection()
        {
            return _selection;
        }

        internal Product BuyProductWithKey(string key)
        {
            Product p;
            if (_selection.TryGetValue(key.ToUpper(), out p))
            {
                if (p.CanAfford(Balance))
                {
                    Balance -= p.price;
                }
                else
                {
                    throw new VendingMachineException("Cannot afford " + p.name);
                }
            }
            else
            {
                throw new VendingMachineException(key + " is not a valid selection.");
            }
            return p;
        }

        internal string GetInfo(string[] args)
        {
            string info = "";
            for (int i = 1; i < args.Length; i++)
            {
                try
                {
                    info += _selection[args[i].ToUpper()].GetDescription() + "\n";
                }
                catch (Exception)
                {
                    throw new VendingMachineException(args[i] + " is not a valid selection.");
                }
            }
            return info;
        }

        internal Dictionary<string, int> GetChange()
        {
            Dictionary<string, int> change = new Dictionary<string, int>();
            while (Balance > 0)
            {
                int c = 0;
                if (Balance >= 1000)
                {
                    change.TryGetValue("1000kr", out c);
                    change["1000kr"] = ++c;
                    Balance -= 1000;
                }
                else if (Balance >= 500)
                {
                    change.TryGetValue("500kr", out c);
                    change["500kr"] = ++c;
                    Balance -= 500;
                }
                else if (Balance >= 100)
                {
                    change.TryGetValue("100kr", out c);
                    change["100kr"] = ++c;
                    Balance -= 100;
                }
                else if (Balance >= 50)
                {
                    change.TryGetValue("50kr", out c);
                    change["50kr"] = ++c;
                    Balance -= 50;
                }
                else if (Balance >= 20)
                {
                    change.TryGetValue("20kr", out c);
                    change["20kr"] = ++c;
                    Balance -= 20;
                }
                else if (Balance >= 10)
                {
                    change.TryGetValue("10kr", out c);
                    change["10kr"] = ++c;
                    Balance -= 10;
                }
                else if (Balance >= 5)
                {
                    change.TryGetValue("5kr", out c);
                    change["5kr"] = ++c;
                    Balance -= 5;
                }
                else
                {
                    change.TryGetValue("1kr", out c);
                    change["1kr"] = ++c;
                    Balance -= 1;
                }
            }
            return change;
        }

        internal int AddMoney(string[] args)
        {
            int totalDeposit = 0;
            for (int i = 1; i < args.Count(); i++)
            {
                int value = 0;
                int.TryParse(args[i].Replace("kr", ""), out value);
                if (_allowed.Contains(value))
                {
                    totalDeposit += value;
                }
                else
                {
                    throw new VendingMachineException($"\"{args[i]}\" is not valid coinage..");
                }
            }
            Balance += totalDeposit;
            return totalDeposit;
        }

        [Serializable]
        public class VendingMachineException : Exception
        {
            public VendingMachineException()
            {
            }

            public VendingMachineException(string message) : base(message)
            {
            }

            public VendingMachineException(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected VendingMachineException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }
}
