using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VendingMachine.Tests
{
    [TestClass()]
    public class VendingMachineTests
    {
        [TestMethod()]
        public void UseTest()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                Product prod = new Drink("Cola", 4, "A soda.");
                prod.Use();
                string expected = string.Format("You drank the Cola", Environment.NewLine);
                Assert.AreEqual<string>(expected, sw.ToString());
            }
        }

        [TestMethod()]
        public void ExamineTest()
        {
            Product prod = new Drink("Cola", 4, "A soda.");
            string expected = "Cola (4kr): A soda.";
            Assert.AreEqual<string>(expected, prod.Examine());
        }

        [TestMethod()]
        public void BuyTest()
        {
            Product prod = new Drink("Cola", 5, "A soda.");
            VendingMachine vm = new VendingMachine(new List<Product>() { prod });
            vm.InsertCash("5kr");
            Assert.IsTrue(vm.BuyProduct(prod));
            Assert.IsFalse(vm.BuyProduct(prod));
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception), "Not enough money exception.")]
        public void PurchaseWithNotEnoughMoneyTest()
        {
            int money = 3;
            Product prod = new Drink("Cola", 5, "A soda.");
            prod.Purchase(money);
            Assert.Fail(); // Should not happen since exception should have been thrown already.
        }

        [TestMethod()]
        public void PurchaseWithEnoughMoneyTest()
        {
            int money = 5;
            Product prod = new Drink("Cola", 5, "A soda.");
            Assert.AreEqual(0, prod.Purchase(money));
        }

        [TestMethod()]
        public void insertMoneyTest()
        {
            VendingMachine vm = new VendingMachine(new List<Product>());
            vm.InsertCash("5kr");
            int expected = 5;
            Assert.AreEqual<int>(expected, vm.MoneyAmountInPool);
        }

        [TestMethod()]
        public void GetChangeTest()
        {
            VendingMachine vm = new VendingMachine(new List<Product>());
            vm.InsertCash("50kr");
            vm.InsertCash("5kr");
            vm.InsertCash("1kr");
            List<string> change = vm.getChange();
            Assert.AreEqual<int>(3, change.Count);
        }
    }
}