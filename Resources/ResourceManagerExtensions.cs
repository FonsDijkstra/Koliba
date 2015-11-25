using System.Collections.Generic;
using System.Globalization;
using System.Resources;

namespace Koliba.Resources
{
    public static class ResourceManagerExtensions
    {
        public static IEnumerable<KeyValuePair<string, object>> AsEnumerable(this ResourceManager manager, CultureInfo culture)
        {
            var iterator = manager.GetResourceSet(culture, true, true).GetEnumerator();
            while (iterator.MoveNext())
            {
                yield return new KeyValuePair<string, object>(iterator.Key as string, iterator.Value);
            }
        }
    }
}
