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
            Assert.IsTrue(vm.buyProduct(prod));
            Assert.IsFalse(vm.buyProduct(prod));
        }

        [TestMethod()]
        public void insertMoneyTest()
        {
            VendingMachine vm = new VendingMachine(new List<Product>());
            vm.InsertCash("5kr");
            int expected = 5;
            Assert.AreEqual<int>(expected, vm.MoneyAmountInPool);
        }
    }
}