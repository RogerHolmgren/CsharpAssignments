using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public abstract class Product
    {
        public string description { get; internal set; }
        public int price { get; internal set; }
        public string name { get; internal set; }

        public Product(string name, int price, string description) {
            this.name = name;
            this.price = price;
            this.description = description;
        }

        /// <summary>
        /// Pass in a money amount to try to purchase the product.
        /// </summary>
        /// <param name="moneyAmount"></param>
        /// <returns>The amount of change you get back after the purchase</returns>
        public int Purchase(int moneyAmount)
        {
            if (moneyAmount < price)
            {
                throw new Exception($"Cannot afford to buy product {name}, money: {moneyAmount} is less than required price: {price}!");
            }
            return moneyAmount - price;
        }

        public string Examine()
        {
            return $"{name} ({price}kr): {description}";
        }

        public abstract void Use();
    }
}
