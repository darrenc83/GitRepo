using System;
using System.ComponentModel;

namespace YPMMS.Shared.Core.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Get an item'sdescription from the DescriptionAttribute
        /// </summary>
        /// <param name="enumValue">Item to retrieve description for</param>
        /// <returns>Value of DescriptionAttribute, or a standard ToString() conversion if none exists</returns>
        public static string GetDescription(this Enum enumValue)
        {
            var attributes = GetAttributes<DescriptionAttribute>(enumValue);
            return (attributes.Length > 0) ? attributes[0].Description : enumValue.ToString();
        }

        /// <summary>
        /// Find out whether an enum value has a specified attribute
        /// </summary>
        /// <typeparam name="T">Attribute type</typeparam>
        /// <param name="enumValue">Item to check for attribute</param>
        /// <returns>True if this enum value has this attribute</returns>
        public static bool HasAttribute<T>(this Enum enumValue) where T : Attribute
        {
            var attributes = GetAttributes<T>(enumValue);
            return (attributes.Length > 0);
        }

        private static T[] GetAttributes<T>(Enum enumValue) where T : Attribute
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            return (T[])field.GetCustomAttributes(typeof(T), false);
        }
    }
}