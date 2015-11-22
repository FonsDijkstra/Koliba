using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Koliba.Apps.Controllers
{
    public class CustomerController : Controller
    {
        [HttpGet]
        [Route("home/index")]
        public ActionResult Index()
        {
            return View();
        }
    }
}