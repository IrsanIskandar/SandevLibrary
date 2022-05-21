using SandevLibrary.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SandevLibrary.Extensions
{
    public class AttributeExtensions
    {
        private static T GetPropertyClass<T>() where T : new()
        {
            T item = new T();
            string fieldName = string.Empty;

            try
            {
                IList<PropertyInfo> propertyInfos = typeof(T).GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.IgnoreCase);
                PropertyInfo modelField = null;
                
                PropertyInfo property = null;
                foreach (var type in propertyInfos)
                {
                    fieldName = type.Name.ToString();
                    modelField = typeof(T).GetProperty(fieldName, BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (fieldName != string.Empty)
                    {
                        if (type.PropertyType == typeof(string))
                        {
                            type.SetValue(item, type.Name, null);
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

        private string ResultIso(int length, ISOPosition position, char padString = '0')
        {
            string ResultIsoString = string.Empty;

            if (position == ISOPosition.Left)
                return ResultIsoString.PadLeft(length, padString);
            else
                return ResultIsoString.PadRight(length, padString);
        }

        private static object GetAttribute(MemberInfo mi, Type t)
        {
            object[] objs = mi.GetCustomAttributes(t, true);

            if (objs == null || objs.Length < 1)
                return null;

            return objs[0];
        }

        public static T GetAttribute<T>(MemberInfo mi)
        {
            return (T)GetAttribute(mi, typeof(T));
        }

        public delegate TResult GetValue_t<in T, out TResult>(T arg1);

        public static TValue GetAttributValue<TAttribute, TValue>(MemberInfo mi, GetValue_t<TAttribute, TValue> value) where TAttribute : Attribute, new()
        {
            TAttribute[] objAtts = (TAttribute[])mi.GetCustomAttributes(typeof(TAttribute), true);
            TAttribute att = new TAttribute();
            if (objAtts != null)
            {
                foreach (var item in objAtts)
                {
                    if (item != null)
                        att = item;
                    else
                        att = default(TAttribute);
                }

                if (att != null)
                {
                    return value(att);
                }
            }

            return default(TValue);
        }
    }
}
