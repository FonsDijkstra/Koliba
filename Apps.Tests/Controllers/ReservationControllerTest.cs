using Koliba.Apps.Controllers;
using System;
using Xunit;

namespace Koliba.Apps.Tests.Controllers
{
    public class ReservationControllerTest
    {
        private readonly ReservationController sut;

        public ReservationControllerTest()
        {
            sut = new ReservationController();
        }

        [Fact]
        public void GetOpeningTimes_not_in_past()
        {
            Assert.All(sut.GetOpeningTimes(), ot => Assert.True(ot.Start.Date >= DateTime.Today));
        }
    }
}
