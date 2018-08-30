using MetroAppTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MetroApp.Tests
{
    [TestClass()]
    public class StopsTests
    {

        [TestMethod()]
        public void GetNearStopsTest()
        {
            FakeStopsProvider fakeStopsProvider = new FakeStopsProvider();
            Stops.StopsProvider = fakeStopsProvider;
            Stop.LinesProvider = fakeStopsProvider;

            string stopsModel = System.IO.File.ReadAllText(@"./../../StopsModel.txt");
            string linesModel = System.IO.File.ReadAllText(@"./../../linesModel.txt");

            fakeStopsProvider.StopsToReturn = stopsModel;
            fakeStopsProvider.LinesToReturn = linesModel;

            Stops target = new Stops();

            List<Stop> result = target.GetNearStops();

            // No Stop duplicated
            Assert.AreEqual(2, result.Count);

            // No Lines duplicated
            Assert.AreEqual(4, result[0].Lines.Count);

            // Synchro on Lines & LinesId
            Assert.AreEqual(result[0].Lines.Length, result[0].Lines.Count);

            // No wierd effect when not duplicated
            Assert.AreEqual(4, result[1].Lines.Count);
        }
    }
}