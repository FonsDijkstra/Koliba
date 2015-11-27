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
        public void Dates_not_in_past()
        {
            var now = DateTime.Now;
            Assert.All(sut.Dates(3), date => Assert.True(date.Start >= now));
        }
    }
}
