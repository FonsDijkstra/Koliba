using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Koliba.Resources;
using System.Globalization;
using System.Collections;
using System.Threading;

namespace Koliba.Resources.Test
{
    public class ResourcesTest
    {
        [Fact]
        public void Has_nl_translation()
        {
            Assert.NotNull(Resources.ResourceManager.GetResourceSet(CultureInfo.GetCultureInfo("nl"), true, false));
        }

        [Fact]
        public void All_resources_have_translations()
        {
            var defaults = Resources.ResourceManager.GetResourceSet(CultureInfo.GetCultureInfo("en"), true, true);
            var overrides = new[] {
                Resources.ResourceManager.GetResourceSet(CultureInfo.GetCultureInfo("nl"), true, false),
            };
            foreach (var resource in defaults)
            {
                var key = ((DictionaryEntry)resource).Key as string;
                Assert.All(overrides, @override => Assert.NotNull(@override.GetObject(key)));
            }
        }
    }
}
