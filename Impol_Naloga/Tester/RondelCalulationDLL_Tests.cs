using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tester
{
    [TestClass]
    public class RondelCalulationDLL_Tests
    {
        [TestMethod]
        public void CalculationTest()
        {
            double width = 10;
            double length = 20;
            double r = 3;
            double minDistanceBetween = 0.5;
            double minDistanceFromEdges = 0.5;

            int maxNumberOfRondels = RondelCalculationDLL.RondelCalculation.Calculate(width, length, r, minDistanceBetween, minDistanceFromEdges);
        }
    }
}
