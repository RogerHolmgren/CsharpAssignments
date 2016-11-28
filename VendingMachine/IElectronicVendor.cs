using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    interface IElectronicVendor
    {
        void InsertCash(string denomination);
        bool BuyProduct(Product prod);
        List<string> getChange();
        Dictionary<string, Product> GetSelection();
    }
}
