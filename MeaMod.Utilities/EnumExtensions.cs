using System;
using System.ComponentModel;
using System.Reflection;

namespace MeaMod.Utilities
{
    /// <summary>
    /// Description attribute extension for Enum
    /// </summary>
    /// 
    public static class EnumExtensions
    {
        /// <summary>
        /// Return the enum description attribute
        /// </summary>
        // <param name="value">Enum value</param>
        /// <returns>Description String</returns>
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }
    }
}
