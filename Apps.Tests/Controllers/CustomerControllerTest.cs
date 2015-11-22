using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Koliba.Apps.Controllers;
using Xunit;

namespace Koliba.Apps.Tests.Controllers
{
    public class CustomerControllerTest
    {
        readonly CustomerController sut;

        public CustomerControllerTest()
        {
            sut = new CustomerController();
        }

        [Fact]
        public void Index_returns_view()
        {
            Assert.NotNull(sut.Index() is ViewResult);
        }
    }
}
