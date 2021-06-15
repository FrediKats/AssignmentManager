using System;
using System.ComponentModel;
using System.Reflection;

namespace AssignmentManager.Server.Extensions
{
    public static class EnumExtensions
    {
        public static string ToDescriptionString<TEnum>(this TEnum @enum)
        {
            FieldInfo info = @enum.GetType().GetField(@enum.ToString());
            var attributes = (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes == null) throw new Exception($"{@enum.GetType()} hasn't {@enum} value");
            return attributes?[0].Description ?? @enum.ToString();
        }
    }
}