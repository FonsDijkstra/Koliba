using Koliba.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Koliba.WebApi.Controllers
{
    public class ReservationController : ApiController
    {
        public IEnumerable<OpeningTime> GetOpeningTimes()
        {
            return new[]
            {
                new OpeningTime { Start = DateTime.Today.AddHours(14), Duration = new TimeSpan(8, 0, 0) },
                new OpeningTime { Start = DateTime.Today.AddDays(1).AddHours(12), Duration = new TimeSpan(10, 0, 0) },
            };
        }
    }
}
