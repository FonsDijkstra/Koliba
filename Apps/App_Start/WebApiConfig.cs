using System.Web.Http;
using Koliba.Apps.Filters;

namespace Koliba.Apps
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Filters.Add(new CultureInfoFilterAttribute());
        }
    }
}
