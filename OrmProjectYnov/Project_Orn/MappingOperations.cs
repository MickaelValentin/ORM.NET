using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Project_Orn
{
    /// <summary>
    /// Toutes les operations de Mapping passe par cette classe
    /// </summary>
    internal class MappingOperations
    {
        /// <summary>
        /// Retourne la list des donnes map en fonction de l objet passe en parametre.
        /// Une instance de l objet est cree puis l ajout des prorietes.
        /// Fini par ajoute l instance dans la list
        /// </summary>
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

        /// <summary>
        /// Retourne l objet MappingObject pour MySQL.
        /// </summary>
        internal static MappingObject GetTypeOfProMySQL<T>(T c)
        {
            MappingObject mappingObject = new MappingObject();
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

        /// <summary>
        /// Retourne l objet MappingObject pour Post Gre.
        /// </summary>
        internal static MappingObject GetTypeOfProPostGre<T>(T c)
        {
            MappingObject mappingObject = new MappingObject();
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

        /// <summary>
        /// Retourne l objet MappingObject pour SQL Server.
        /// </summary>
        internal static MappingObject GetTypeOfProSQLServer<T>(T c)
        {
            MappingObject mappingObject = new MappingObject();
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
                        propertyAttributes.TypeInfo = "TEXT";
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
                        propertyAttributes.TypeInfo = "REAL";
                        break;
                    }
                    case "Double":
                    {
                        propertyAttributes.TypeInfo = "FLOAT";
                        break;
                    }
                }

                mappingObject.PropertiesAttributes.Add(propertyAttributes);
            }


            return mappingObject;
        }
    }
}