using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class VendingMachine : IElectronicVendor
    {
        public readonly Dictionary<string, int> Allowed = new Dictionary<string, int>(){
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
        public int MoneyAmountInPool { get; private set; }

        public VendingMachine(List<Product> list)
        {
            this.list = list;
        }

        public void InsertCash(string cash)
        {
            if (Allowed.ContainsKey(cash))
            {
                MoneyAmountInPool += Allowed[cash];
            }
            else
            {
                throw new Exception($"\"{cash}\" is not valid coinage..");
            }
        }

        public bool BuyProduct(Product prod)
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

        public List<Product> GetSelection()
        {
            throw new NotImplementedException();
        }
    }
}
