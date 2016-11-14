using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class VendingMachine : IElectronicVendor
    {
        public readonly Dictionary<string, int> coinage = new Dictionary<string, int>(){
            {   "1kr", 1 },
            {   "5kr", 5 },
            {  "10kr", 10 },
            {  "20kr", 20 },
            {  "50kr", 50 },
            { "100kr", 100 },
            { "500kr", 500 },
            {"1000kr", 1000 }
        };
        private List<Product> list;

        public VendingMachine(List<Product> list)
        {
            this.list = list;
        }

        public int MoneyAmountInPool { get; private set; }

        public void InsertCash(string cash)
        {
            if (coinage.ContainsKey(cash))
            {
                MoneyAmountInPool += coinage[cash];
            } else
            {
                throw new Exception($"\"{cash}\" is not valid coinage..");
            }
        }

        public bool buyProduct(Product prod)
        {
            try
            {
                MoneyAmountInPool = prod.Purchase(MoneyAmountInPool);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
