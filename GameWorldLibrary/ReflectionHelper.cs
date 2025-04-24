using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWorldLibrary
{
    public static class ReflectionHelper
    {
        public static string GetDisplayName(object obj)
        {
            if (obj == null) return "Unknown";

            var nameProp = obj.GetType().GetProperty("Name");
            if (nameProp != null)
            {
                var value = nameProp.GetValue(obj);
                if (value != null)
                    return value.ToString();
            }

            return obj.GetType().Name;
        }
    }
}
