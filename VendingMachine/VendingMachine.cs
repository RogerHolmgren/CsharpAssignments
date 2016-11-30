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
    public class VendingMachine : IElectronicVendor
    {
        public readonly Dictionary<string, int> AllowedCurrency = new Dictionary<string, int>(){
            {   "1kr", 1 },
            {   "5kr", 5 },
            {  "10kr", 10 },
            {  "20kr", 20 },
            {  "50kr", 50 },
            { "100kr", 100 },
            { "500kr", 500 },
            {"1000kr", 1000 }
        };

        internal bool BuyProductWithKey(string key)
        {
            Product p;
            if (_selection.TryGetValue(key.ToUpper(), out p))
            {
                return BuyProduct(p);
            }
            else
            {
                throw new InvalidChoiceException(key);
            }
        }

        private Dictionary<string, Product> _selection = new Dictionary<string, Product>();
        public int MoneyAmountInPool { get; private set; }
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
                Console.Write(currentRow = prod.Key + ": " + prod.Value.name.PadRight(LongestProductName.Length +1));  
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Add money by supplying a string with a currency denomination.
        /// </summary>
        /// <param name="cash"></param>
        public void InsertCash(string cash)
        {
            if (AllowedCurrency.ContainsKey(cash))
            {
                MoneyAmountInPool += AllowedCurrency[cash];
            }
            else
            {
                throw new Exception($"\"{cash}\" is not valid coinage..");
            }
        }

        /// <summary>
        /// Attempts to buy the supplied product.
        /// </summary>
        /// <param name="prod"></param>
        /// <returns>Boolean: true if purchase was successful, false if not.</returns>
        public bool BuyProduct(Product prod)
        {
            try
            {
                int returnedChange = prod.Purchase(MoneyAmountInPool);
                MoneyAmountInPool = returnedChange;
                return true;
            }
            catch (Exception)
            {
                // Exception in this case means that Purchase didn't work,
                // so simply return false and dont change anything.
                return false;
            }
        }

        public List<string> getChange()
        {
            List<string> change = new List<string>();
            while (MoneyAmountInPool > 0)
            {
                if (MoneyAmountInPool >= 1000)
                {
                    change.Add("1000kr");
                    MoneyAmountInPool -= 1000;
                }
                else if (MoneyAmountInPool >= 500)
                {
                    change.Add("500kr");
                    MoneyAmountInPool -= 500;
                }
                else if (MoneyAmountInPool >= 100)
                {
                    change.Add("100kr");
                    MoneyAmountInPool -= 100;
                }
                else if (MoneyAmountInPool >= 50)
                {
                    change.Add("50kr");
                    MoneyAmountInPool -= 50;
                }
                else if (MoneyAmountInPool >= 20)
                {
                    change.Add("20kr");
                    MoneyAmountInPool -= 20;
                }
                else if (MoneyAmountInPool >= 10)
                {
                    change.Add("10kr");
                    MoneyAmountInPool -= 10;
                }
                else if (MoneyAmountInPool >= 5)
                {
                    change.Add("5kr");
                    MoneyAmountInPool -= 5;
                }
                else
                {
                    change.Add("1kr");
                    MoneyAmountInPool -= 1;
                }
            }
            return change;
        }

        public void Start()
        {
            string choice = "";
            bool quit = false;
            PrintSelectionGrid();
            do
            {
                choice = Console.ReadLine().ToLower();
                Console.Clear();
                PrintSelectionGrid();
                string[] choices = choice.Split();
                switch (choices[0])
                {
                    case "add":
                        AddMoney(choices);
                        break;
                    case "buy":
                        Buy(choices);
                        break;
                    case "get":

                        break;
                    case "quit":
                    case "q":
                        quit = true;
                        break;
                    default:
                        break;
                } 
            } while (!quit);
        }

        private void AddMoney(string[] args)
        {
            for (int i = 1; i < args.Count(); i++)
            {
                InsertCash(args[i]);
            }
        }

        private void Buy(string[] args)
        {
            try
            {
                if (BuyProductWithKey(args[1]))
                {
                    Console.WriteLine("You bought " + _selection[args[1].ToUpper()].name);
                }
                else
                {
                    Console.WriteLine("You cannot afford " + _selection[args[1].ToUpper()].name);
                }
            }
            catch (InvalidChoiceException e)
            {
                Console.WriteLine(e.Message + " is not a valid choice.");
            }
        }

        [Serializable]
        private class InvalidChoiceException : Exception
        {
            public InvalidChoiceException()
            {
            }

            public InvalidChoiceException(string message) : base(message)
            {
            }

            public InvalidChoiceException(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected InvalidChoiceException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }
}
