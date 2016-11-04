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
            int distance = PhysicsEngine.getDistance(45, 56);
            Assert.IsTrue(distance == 320);
        }

        [TestMethod]
        public void TestZeroVelocity()
        {
            int distance = PhysicsEngine.getDistance(45, 0);
            Assert.IsTrue(distance == 0);
        }

        [TestMethod]
        public void TestZeroAngle()
        {
            int distance = PhysicsEngine.getDistance(0, 56);
            Assert.IsTrue(distance == 0);
        }

        [TestMethod]
        public void TestAngleAbove90Degrees()
        {
            int distance = PhysicsEngine.getDistance(91, 56);
            Assert.IsTrue(distance == -10);
        }
    }
}
