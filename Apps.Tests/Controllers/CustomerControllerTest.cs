using System.Web.Mvc;
using Xunit;
using Koliba.Apps.Controllers;

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
