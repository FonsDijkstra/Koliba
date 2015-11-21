using Koliba.WebApi.Controllers;
using System;
using Xunit;

namespace Koliba.WebApi.Tests
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
