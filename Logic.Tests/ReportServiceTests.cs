// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data.ViewModel;
using Logic.Services.Report;
using Moq;
using NUnit.Framework;
using Repository;

namespace Logic.Tests
{
    [TestFixture]
    public class ReportServiceTests
    {
        [Test]
        public void GetVicState_returns_victorian_state()
        {
            var data = new List<State>
            {
                new State { StateId = 1, StateName = "Victoria" },
                new State { StateId = 2, StateName = "NSW" },
                new State { StateId = 3, StateName = "NotVictora" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<State>>();
            mockSet.As<IQueryable<State>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<State>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<State>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<State>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LGAContext>();
            mockContext.Setup(c => c.States).Returns(mockSet.Object);

            var service = new ReportService(mockContext.Object);
            var stateId = service.VicStateId;

            Assert.AreEqual(1, stateId);
        }
    }
}
