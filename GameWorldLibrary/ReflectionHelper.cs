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
            if (obj == null)
                return "Unknown";

            var nameProp = obj.GetType().GetProperty("Name");

            if (nameProp?.GetValue(obj) is string name)
                return name;

            return obj.GetType().Name;
        }
    }
}
