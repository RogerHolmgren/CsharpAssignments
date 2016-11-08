using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Golf;

namespace GolfTest
{
    [TestClass]
    public class PhysicsEngineTest
    {
        [TestMethod]
        public void TestDistance()
        {
            int distance = new PhysicsEngine().getDistance(45, 56);
            Assert.IsTrue(distance == 320);
        }
    }
}
