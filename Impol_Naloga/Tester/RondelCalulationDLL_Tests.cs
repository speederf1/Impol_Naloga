using Microsoft.VisualStudio.TestTools.UnitTesting;
using RondelCalculationDLL;
using System.Collections.Generic;

namespace Tester
{
    [TestClass]
    public class RondelCalulationDLL_Tests
    {
        [DataRow(5, 10, 0.75, 0, 0, 18)]
        [DataRow(5, 10, 0.75, 0.2, 0.2, 15)]
        [DataRow(20, 100, 1, 0.3, 0.3, 383)]
        [DataRow(257, 157, 1.784, 0, 0, 3611)]
        [DataTestMethod]
        public void CalculationTest(double width, double length, double r, double minDistanceBetween, double minDistanceFromEdges, int result)
        {
            List<Rondel> rondels = RondelCalculation.Calculate(width, length, r, minDistanceBetween, minDistanceFromEdges);

            Assert.AreEqual(rondels.Count, result);
        }
    }
}
