using Koliba.WebApi.Controllers;
using System;
using Xunit;

namespace Koliba.WebApi.Tests
{
    public class ReservationControllerTest
    {
        [Fact]
        public void GetOpeningTimes()
        {
            var sut = new ReservationController();
            Assert.All(sut.GetOpeningTimes(), ot => Assert.True(ot.Start.Date >= DateTime.Today));
        }
    }
}
