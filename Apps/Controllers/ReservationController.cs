using Koliba.Apps.Models;
using Koliba.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Http;

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
            if (ActionContext.Request.Headers.TryGetValues("Accept-Language", out header) && header.Any())
            {
                culture = CultureInfo.GetCultureInfo(header.First());
            }
            else
            {
                culture = CultureInfo.InvariantCulture;
            }

            return Resources.Resources.ResourceManager
                .AsEnumerable(culture)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

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
