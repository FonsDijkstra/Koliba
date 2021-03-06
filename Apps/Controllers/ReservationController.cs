﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using Koliba.Resources;
using Koliba.Business;

namespace Koliba.Apps.Controllers
{
    public class ReservationController : ApiController
    {
        [HttpGet]
        [Route("api/reservation/resources")]
        public IDictionary<string, object> GetResources()
        {
            return Resources.Resources.ResourceManager
                .AsEnumerable(CultureInfo.CurrentUICulture)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        [HttpGet]
        [Route("api/reservation/dates/{nofDaysAhead}")]
        public IEnumerable<OpeningDate> Dates(int nofDaysAhead)
        {
            return new OpeningTimes(OpeningTimes.SCHEDULE).OpeningDates(DateTime.Now, nofDaysAhead);
        }

        [HttpPost]
        [Route("api/reservation/times")]
        public IEnumerable<ReservationTime> Times([FromBody] OpeningDate date)
        {
            return new Reservations().ReservationTimes(DateTime.Now.Add(TimeSpan.FromMinutes(30)), date);
        }
    }
}
