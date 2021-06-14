using System;
using System.ComponentModel;

namespace AssignmentManager.Server.Mapping
{
    public static class MappingHelper
    {
        public static string StringValueOf(Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[]) fi?.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes == null)
                throw new Exception($"{value.GetType()} hasn't {value} value");
            if (attributes.Length > 0)
                return attributes[0].Description;
            return value.ToString();
        }
    }
}