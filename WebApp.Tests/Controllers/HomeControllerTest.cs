using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Koliba.WebApp.Controllers;
using Xunit;

namespace Koliba.WebApp.Tests.Controllers
{
    public class HomeControllerTest
    {
        readonly HomeController sut;

        public HomeControllerTest()
        {
            sut = new HomeController();
        }

        [Fact]
        public void Index_returns_view()
        {
            Assert.NotNull(sut.Index() is ViewResult);
        }
    }
}
