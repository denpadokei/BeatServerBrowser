using System;
using System.ComponentModel;

namespace BeatServerBrowser.Core.Extentions
{
    public static class EnumExtention
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (attribute != null) {
                return attribute.Description;
            }
            else {
                return value.ToString();
            }
        }

    }
}




