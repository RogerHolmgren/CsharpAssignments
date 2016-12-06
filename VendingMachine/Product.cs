using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public abstract class Product
    {
        public string description { get; private set; }
        public int price { get; private set; }
        public string name { get; private set; }

        public Product(string name, int price, string description)
        {
            this.name = name;
            this.price = price;
            this.description = description;
        }

        public bool CanAfford(int moneyAmount)
        {
            return moneyAmount >= price;
        }

        /// <summary>
        /// A descriptive string of all the class fields.
        /// </summary>
        /// <returns>The string</returns>
        public string GetDescription()
        {
            return $"{name} ({price}kr): {description}";
        }

        /// <summary>
        /// Usage of the product
        /// </summary>
        public abstract void Use();
    }
}
