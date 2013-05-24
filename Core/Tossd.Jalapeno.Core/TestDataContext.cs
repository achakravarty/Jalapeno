using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tossd.Jalapeno.Core
{
    public class TestDataContext
    {
        internal static Dictionary<string, object> DataContext { get; set; }

        internal TestDataContext()
        {
            DataContext = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
        }

        public static void Add(string key, object data)
        {
            DataContext[key] = data;
        }

        public static bool Delete(string key)
        {
            return DataContext.Remove(key);
        }

        public static T Get<T>(string key)
        {
            var data = DataContext[key];
            return data == null ? default(T) : (T)data;
        }
    }
}
