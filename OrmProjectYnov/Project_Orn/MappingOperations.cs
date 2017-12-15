﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Project_Orn
{
    internal class MappingOperations
    {
        internal static List<T> MapList<T>(DataTable dt, T c)
        {
            List<T> list = new List<T>();

            PropertyInfo[] propertyInfoObject = c.GetType().GetProperties();


            foreach (DataRow dr in dt.Rows)
            {
                T t = Activator.CreateInstance<T>();

                foreach (PropertyInfo fi in propertyInfoObject)
                {
                    if (fi.PropertyType.Name.Equals("Boolean"))
                    {
                        int temp = Convert.ToInt32(dr[fi.Name]);
                        fi.SetValue(t, Convert.ToBoolean(temp));
                    }
                    else
                    {
                        fi.SetValue(t, dr[fi.Name]);
                    }
                }

                list.Add(t);

            }

            return list;
        }

        internal static MappingObject GetTypeOfProMySQL<T>(T c)
        {
            MappingObject mappingObject = new MappingObject();
            Console.WriteLine(c.GetType().Name);
            mappingObject.ObjectName = c.GetType().Name;
            mappingObject.PropertiesAttributes = new List<PropertyAttributes>();
            foreach (PropertyInfo propertyInfoObject in c.GetType().GetProperties())
            {
                PropertyAttributes propertyAttributes = new PropertyAttributes
                {
                    NameInfo = propertyInfoObject.Name,

                    ValueInfo = propertyInfoObject.GetValue(c)
                };

                switch (propertyInfoObject.PropertyType.Name.ToString())
                {
                    case "Int32":
                        {
                            propertyAttributes.TypeInfo = "INT";
                            break;
                        }
                    case "String":
                        {
                            propertyAttributes.TypeInfo = "MEDIUMTEXT";
                            break;
                        }
                    case "DateTime":
                        {
                            propertyAttributes.TypeInfo = "DATETIME";
                            break;
                        }
                    case "Boolean":
                        {
                            propertyAttributes.TypeInfo = "BIT";
                            break;
                        }
                    case "Single":
                        {
                            propertyAttributes.TypeInfo = "FLOAT";
                            break;
                        }
                    case "Double":
                        {
                            propertyAttributes.TypeInfo = "DOUBLE";
                            break;
                        }
                }

                mappingObject.PropertiesAttributes.Add(propertyAttributes);
            }



            return mappingObject;

        }

        internal static MappingObject GetTypeOfProPostGre<T>(T c)
        {
            MappingObject mappingObject = new MappingObject();
            Console.WriteLine(c.GetType().Name);
            mappingObject.ObjectName = c.GetType().Name;
            mappingObject.PropertiesAttributes = new List<PropertyAttributes>();
            foreach (PropertyInfo propertyInfoObject in c.GetType().GetProperties())
            {
                PropertyAttributes propertyAttributes = new PropertyAttributes
                {
                    NameInfo = propertyInfoObject.Name,

                    ValueInfo = propertyInfoObject.GetValue(c)
                };

                switch (propertyInfoObject.PropertyType.Name.ToString())
                {
                    case "Int32":
                        {
                            propertyAttributes.TypeInfo = "int";
                            break;
                        }
                    case "String":
                        {
                            propertyAttributes.TypeInfo = "text";
                            break;
                        }
                    case "DateTime":
                        {
                            propertyAttributes.TypeInfo = "date";
                            break;
                        }
                    case "Boolean":
                        {
                            propertyAttributes.TypeInfo = "boolean";
                            break;
                        }
                    case "Single":
                        {
                            propertyAttributes.TypeInfo = "real";
                            break;
                        }
                    case "Double":
                        {
                            propertyAttributes.TypeInfo = "double precision";
                            break;
                        }
                }

                mappingObject.PropertiesAttributes.Add(propertyAttributes);
            }



            return mappingObject;

        }
    }
}