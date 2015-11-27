﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using Koliba.Resources;
using Koliba.Business;
using System.Threading;

namespace Koliba.Apps.Controllers
{
    public class ReservationController : ApiController
    {
        [HttpGet]
        [Route("api/reservation/resources")]
        public IDictionary<string, object> GetResources()
        {
            CultureInfo culture;
            IEnumerable<string> header;
            if (ActionContext.Request.Headers.TryGetValues("Accept-Language", out header) && header.Any()) {
                culture = CultureInfo.GetCultureInfo(header.First());
            } else {
                culture = CultureInfo.InvariantCulture;
            }
            Thread.CurrentThread.CurrentUICulture = culture;
            // TODO: splitsen
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
    }
}
