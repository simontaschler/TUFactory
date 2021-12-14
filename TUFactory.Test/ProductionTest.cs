using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TUFactory.Lib;

namespace TUFactory.Test
{
    [TestClass]
    public class ProductionTest
    {
        [TestMethod]
        public void TestLastTimeStep()
        {
            var orderLines = ResourceHelper.GetEmbeddedResourceLines(Assembly.GetExecutingAssembly(), "TUFactory.Test.TeileListe_neu.csv");
            var qualityManagement = new QualityManagement(.95, 0, 0);
            var management = new Management(qualityManagement);
            management.AddMachine(new TurningMachine(1, 0, 75, .5, .1, 10, 10));
            management.AddMachine(new MillingMachine(2, 0, 25, 5, 20, 0, 10));
            management.AddMachine(new GrindingMachine(3, 0, .02, 50, 30, 80, 20, 0));

            management.ReadOrders(orderLines);

            for (var timeStep = 0; timeStep <= 30; timeStep++)
            {
                management.Produce(timeStep);
                management.SendToQualityCheck();
                management.SimulatePossibleError(timeStep);
            }

            var allParts = management.AllParts;

            var compStates = new List<int> { 4, 2, 2, 4, 2 };
            var compQualities = new List<double>
            {
                .976733333333333,
                .9834,
                .973728888888889,
                .991111111111111,
                .987777777777778
            };

            Assert.AreEqual(compStates.Count, allParts.Count);

            for (var i = 0; i < compStates.Count; i++) 
            {
                Assert.AreEqual(compStates[i], (int)allParts[i].State);
                Assert.AreEqual(compQualities[i], allParts[i].Quality, 1E-15);
            }
        }
    }
}
