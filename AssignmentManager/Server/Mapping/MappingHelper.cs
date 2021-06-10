using System;
using System.ComponentModel;
using System.Reflection;

namespace AssignmentManager.Server.Mapping
{
    public static class MappingHelper
    {
        public static string StringValueOf(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi?.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes == null)
                throw new Exception($"{value.GetType()} hasn't {value} value");
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
    }
}