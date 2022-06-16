using SandevLibrary.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace SandevLibrary.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="listProperty"></param>
        /// <returns></returns>
        public static List<TValue> ToList<TValue>(this List<TValue> listProperty) where TValue : new()
        {
            IList<PropertyInfo> properties = typeof(TValue).GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.IgnoreCase).ToList();
            List<TValue> result = new List<TValue>();

            for (int i = 0; i < listProperty.Count(); i++)
            {
                var item = ReadAttributeUsage<ISOFixedLengthAttribute, TValue>(properties);
                result.Add(item);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="properties"></param>
        /// <returns></returns>
        private static TValue ReadAttributeUsage<TAttribute, TValue>(IList<PropertyInfo> properties) where TValue : new() where TAttribute : Attribute
        {
            string fieldName = string.Empty;
            TValue item = new TValue();

            try
            {
                //IList<PropertyInfo> propertyInfos = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.IgnoreCase);
                PropertyInfo modelField = null;

                PropertyInfo property = null;
                foreach (var propertyItem in properties)
                {
                    fieldName = propertyItem.Name.ToString();
                    if (!Attribute.IsDefined(propertyItem, typeof(ISOFixedLengthAttribute)))
                    {
                        int lengIso = propertyItem.GetCustomAttribute<ISOFixedLengthAttribute>().LengthIso;
                        ISOPosition isoPosition = propertyItem.GetCustomAttribute<ISOFixedLengthAttribute>().Position;
                        char charater = propertyItem.GetCustomAttribute<ISOFixedLengthAttribute>().CharaterString;
                        string result = propertyItem.GetCustomAttribute<ISOFixedLengthAttribute>().ResultIsoString;

                        if (isoPosition != ISOPosition.Left)
                        {
                            if (fieldName != string.Empty && property.PropertyType == typeof(string))
                            {
                                property.SetValue(item, result.PadLeft(lengIso, charater), null);
                            }
                        }
                        else
                        {
                            if (fieldName != string.Empty && property.PropertyType == typeof(string))
                            {
                                property.SetValue(item, result.PadRight(lengIso, charater), null);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return item;
        }
    }
}
