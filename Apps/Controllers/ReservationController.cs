﻿using Koliba.Apps.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Koliba.Apps.Controllers
{
    public class ReservationController : ApiController
    {
        [HttpGet]
        [Route("api/reservation/openingtimes")]
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