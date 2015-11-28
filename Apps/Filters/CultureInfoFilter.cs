using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Koliba.Apps.Filters
{
    public class CultureInfoFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            CultureInfo culture;
            IEnumerable<string> header;
            if (actionContext.Request.Headers.TryGetValues("Accept-Language", out header) && header.Any()) {
                culture = CultureInfo.GetCultureInfo(header.First());
            } else {
                culture = CultureInfo.InvariantCulture;
            }
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}