using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyList;
using System;
using System.Text;
using System.Threading.Tasks;

namespace MyList.Tests
{
    [TestClass()]
    public class MyListTests
    {
        const string name0 = "Roger";
        const string name1 = "Nils";
        const string name2 = "Kalle";
        const string name3 = "Maria";

        [TestMethod()]
        public void AddTest()
        {
            List<string> myList = new List<string>();
            myList.Add(name0);
            myList.Add(name1);
            myList.Add(name2);
            myList.Add(name3);

        }
    }
}