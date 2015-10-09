using System;
using System.Linq;
using AuctionPlanet.DataAccess.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AuctionPlanet.DataAccess.Entities;

namespace AuctionPlanet.Tests
{
    [TestClass]
    public class TimeTest
    {
        [TestMethod]
        public void DateTimePlusTicksTestMethod()
        {
            ApplicationDbContext db = ApplicationDbContext.Create();
            Lot lot = db.Lots.First();
            
            Assert.IsNotNull(lot.StartTime);

            DateTime startTime = lot.StartTime.Value;
            long durationTicks = lot.Duration;
            DateTime endTime = startTime.AddTicks(durationTicks);

            Assert.IsTrue((endTime - DateTime.Now).Ticks < 0L);
        }
    }
}
