using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace SandevLibrary.Extensions
{
    public static class DataTableServiceExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable table) where T : new()
        {
            IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            List<T> result = new List<T>();

            foreach (var row in table.Rows)
            {
                var item = CreateItemFromRow<T>((DataRow)row, properties);
                result.Add(item);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
        {
            string fieldName = string.Empty;
            T item = new T();
            try
            {
                PropertyInfo modelField = null;
                foreach (var property in properties)
                {
                    fieldName = row[property.Name].ToString();
                    modelField = typeof(T).GetProperty(fieldName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    if (property.PropertyType == typeof(System.DayOfWeek))
                    {
                        DayOfWeek day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), row[property.Name].ToString());
                        property.SetValue(item, day, null);
                    }
                    else
                    {
                        if (fieldName != string.Empty)
                        {
                            if (row[property.Name] == DBNull.Value)
                            {
                                property.SetValue(item, null, null);
                            }
                            else
                            {
                                if (property.PropertyType == typeof(DateTime))
                                {
                                    if (fieldName == string.Empty)
                                    {
                                        property.SetValue(item, null, null);
                                    }
                                    else
                                    {
                                        property.SetValue(item, Convert.ToDateTime(row[property.Name]), null);
                                    }
                                }
                                else if (property.PropertyType == typeof(double))
                                {
                                    if (Convert.ToDouble(fieldName) == 0)
                                    {
                                        property.SetValue(item, null, null);
                                    }
                                    else
                                    {
                                        property.SetValue(item, Convert.ToDouble(row[property.Name]), null);
                                    }
                                }
                                else if (property.PropertyType == typeof(decimal))
                                {
                                    if (Convert.ToDecimal(fieldName) == 0)
                                    {
                                        property.SetValue(item, null, null);
                                    }
                                    else
                                    {
                                        property.SetValue(item, Convert.ToDecimal(row[property.Name]), null);
                                    }
                                }
                                else if (property.PropertyType == typeof(long))
                                {
                                    if (Convert.ToInt64(fieldName) == 0)
                                    {
                                        property.SetValue(item, null, null);
                                    }
                                    else
                                    {
                                        property.SetValue(item, Convert.ToInt64(row[property.Name]), null);
                                    }
                                }
                                else if (property.PropertyType == typeof(int))
                                {
                                    if (Convert.ToInt32(fieldName) == 0)
                                    {
                                        property.SetValue(item, null, null);
                                    }
                                    else
                                    {
                                        property.SetValue(item, Convert.ToInt32(row[property.Name]), null);
                                    }
                                }
                                else
                                {
                                    property.SetValue(item, row[property.Name]);
                                }
                            }
                        }
                        else
                        {
                            property.SetValue(item, null, null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return item;
        }
    }
}
