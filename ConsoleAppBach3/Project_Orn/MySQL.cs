﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;

using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Project_Orn
{
    public static class MySQL
    {

        public static void createSqlTable()
        {
            OdbcConnection conn = new OdbcConnection(


            "DRIVER={MySQL ODBC 5.3 ANSI Driver};" +
                   "SERVER=localhost;" +
                   "DATABASE=orm;" +
                   "USER=root;");

            Console.WriteLine("Créer ->");
            Console.WriteLine("Enter your table name");
            string nomTable = Console.ReadLine();
            Console.WriteLine($"Entrer le nombre de colonnes de la table {nomTable}");
            string nombreColonne = Console.ReadLine();
            int nombre = Int32.Parse(nombreColonne);
            string nomCol;
            string nomAllCol = "";
            string type;
            string taille;

            for (int i = 0; i < nombre; i++)
            {
                Console.WriteLine($"Colonne {i + 1} : ");
                nomCol = Console.ReadLine();

                Console.WriteLine($"Type de la colonne {nomCol} : ");
                type = Console.ReadLine();

                Console.WriteLine($"Taille/Valeur : ");
                taille = Console.ReadLine();

                type.ToLower();

                if (i == 0)
                {
                    nomAllCol = $"{nomCol} {type}({taille})";
                }
                else
                {
                    nomAllCol = $"{nomAllCol},{nomCol} {type}({taille})";
                }

            }

            try
            {
                // 2. Open the connection
                conn.Open();

                //OdbcCommand cmd = new OdbcCommand($"CREATE TABLE {nomTable}()", conn);
                OdbcCommand cmd = new OdbcCommand($"CREATE TABLE {nomTable} ({nomAllCol})", conn);
                cmd.ExecuteNonQuery();

                Console.WriteLine("La table à bien été créer");
            }

            finally
            {


                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public static void selectTable()
        {
            OdbcConnection conn = new OdbcConnection(


           "DRIVER={MySQL ODBC 5.3 ANSI Driver};" +
                  "SERVER=localhost;" +
                  "DATABASE=orm;" +
                  "USER=root;");

            Console.WriteLine("Rechercher ->");
            try
            {
                // 2. Open the connection
                conn.Open();

                Console.WriteLine("Table : ?");
                string nomTable = Console.ReadLine();

                Console.WriteLine("Colonne : ? (* pour tout lister)");
                string nomColonne = Console.ReadLine();

                //OdbcCommand cmd = new OdbcCommand($"CREATE TABLE {nomTable}()", conn);
                OdbcCommand cmd = new OdbcCommand($"SELECT {nomColonne} FROM {nomTable}", conn);
                OdbcDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine(reader[1]);

                }
            }

            finally
            {


                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public static void dropTable()
        {
            OdbcConnection conn = new OdbcConnection(


           "DRIVER={MySQL ODBC 5.3 ANSI Driver};" +
                  "SERVER=localhost;" +
                  "DATABASE=orm;" +
                  "USER=root;");

            Console.WriteLine("Supprimer ->");
            try
            {
                // 2. Open the connection
                conn.Open();

                Console.WriteLine("1 - Supprimer une table");
                Console.WriteLine("2 - Supprimer une colonne");

                string switchString = Console.ReadLine();
                int caseSwitch = Int32.Parse(switchString);

                Console.WriteLine("Table :  ?");
                string nomTable = Console.ReadLine();

                switch (caseSwitch)
                {
                    case 1:
                        OdbcCommand cmdTable = new OdbcCommand($"DROP TABLE IF EXISTS {nomTable}", conn);
                        cmdTable.ExecuteNonQuery();
                        break;
                    case 2:
                        Console.WriteLine("Colonne : ?");
                        string nomColonne = Console.ReadLine();

                        OdbcCommand cmdCol = new OdbcCommand($"ALTER TABLE {nomTable} DROP COLUMN {nomColonne}", conn);
                        cmdCol.ExecuteNonQuery();
                        break;
                    default:
                        Console.WriteLine("Une erreur est survenu");
                        break;
                }




                //cmd.ExecuteNonQuery();

                Console.WriteLine($"La table {nomTable} à bien été suprimer");
                Console.WriteLine("Appuyer sur une touche pour continuer");
                Console.ReadLine();
            }

            finally
            {


                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public static void insert()
        {
            OdbcConnection conn = new OdbcConnection(


           "DRIVER={MySQL ODBC 5.3 ANSI Driver};" +
                  "SERVER=localhost;" +
                  "DATABASE=orm;" +
                  "USER=root;");

            Console.WriteLine("Insérer ->");

            string nomTable;
            string nomColonne;
            string valeur;
            string nombreColonne;
            int nombre;
            string allColonne = "";
            string allValeur = "";

            try
            {
                conn.Open();

                Console.WriteLine("Table : ?");
                nomTable = Console.ReadLine();

                Console.WriteLine("Nombre colonne : ?");
                nombreColonne = Console.ReadLine();
                nombre = Int32.Parse(nombreColonne);

                for (int i = 0; i < nombre; i++)
                {
                    Console.WriteLine($"Nom colonne {i + 1} : ?");
                    nomColonne = Console.ReadLine();

                    Console.WriteLine($"Valeur colonne {i + 1} : ?");
                    valeur = Console.ReadLine();

                    if (i == 0)
                    {
                        allColonne = nomColonne;
                        allValeur = valeur;
                    }
                    else
                    {
                        allColonne = $"{allColonne}, {nomColonne}";
                        allValeur = $"{allValeur}, {valeur}";
                    }


                }

                OdbcCommand cmd = new OdbcCommand($"INSERT INTO {nomTable} ({allColonne}) VALUES ({allValeur})", conn);
                cmd.ExecuteReader();


            }

            finally
            {


                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public static void InsertNextGen<T>(T obj)
        {
            MappingObject objectMapping = new MappingObject();
            objectMapping = MySQL.GetTypeOfPro(obj);
            OdbcConnection conn = new OdbcConnection(
                "DRIVER={MySQL ODBC 5.3 ANSI Driver};" +
                "SERVER=localhost;" +
                "DATABASE=test;" +
                "USER=root;" +
                "PASSWORD=root");

            string req = $" INSERT INTO {objectMapping.ObjectName} VALUES(NULL,";
            for (int i = 0; i < objectMapping.PropertiesAttributes.Count(); i++)
            {
                if (i == objectMapping.PropertiesAttributes.Count() - 1)
                {
                    req += "?";
                }
                else
                {
                    req += "?,";
                }
            }
            req += ")";

            OdbcCommand qureyToInsert = new OdbcCommand(req, conn);
            for (int i = 0; i < objectMapping.PropertiesAttributes.Count(); i++)
            {
                PropertyAttributes infoFormapping = objectMapping.PropertiesAttributes[i];
                qureyToInsert.Parameters.AddWithValue($"{infoFormapping.NameInfo}", infoFormapping.ValueInfo);
            }
            try
            {
                conn.Open();

                //  Prepare command not supported in ODBC
                qureyToInsert.Prepare();
                qureyToInsert.ExecuteNonQuery();
                //  Data to be inserted
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public static void CreateTableNextGen<T>(T obj)
        {
            MappingObject objectMapping = new MappingObject();
            objectMapping = GetTypeOfPro(obj);
            OdbcConnection conn = new OdbcConnection(
                "DRIVER={MySQL ODBC 5.3 ANSI Driver};" +
                "SERVER=localhost;" +
                "DATABASE=test;" +
                "USER=root;" +
                "PASSWORD=root");

            string req = $"CREATE TABLE IF NOT EXISTS {objectMapping.ObjectName}(ID int NOT NULL AUTO_INCREMENT,";
            for (int i = 0; i < objectMapping.PropertiesAttributes.Count(); i++)
            {
                req += $"{objectMapping.PropertiesAttributes[i].NameInfo} {objectMapping.PropertiesAttributes[i].TypeInfo},";
            }
            req += "PRIMARY KEY(ID))";

            try
            {
                conn.Open();
                OdbcCommand cmd = new OdbcCommand(req, conn);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public static List<T> SelectTableNextGen<T>(string column,string value ,T table)
        {

            OdbcConnection conn = new OdbcConnection(
                "DRIVER={MySQL ODBC 5.3 ANSI Driver};" +
                "SERVER=localhost;" +
                "DATABASE=test;" +
                "USER=root;" +
                "PASSWORD=root");
            

                string req;
            if (table.GetType().Name == null)
            {
                Console.WriteLine("obj not found");
                return null;
            }
            bool isAProperty = false;
            foreach (PropertyInfo item in table.GetType().GetProperties())
            {
                if (column.Equals(item.Name))
                {
                    isAProperty = true;
                    break;
                }

            }
            if (isAProperty == false)
            {
                Console.WriteLine("property obj not found");
                return null;
            }
          
            if (value == null || column == null)
            {
                    req = $"SELECT * FROM {table.GetType().Name.ToString()}";
              }

            else {
                    req = $"SELECT * FROM {table.GetType().Name.ToString()} WHERE {column} = @Value";
                }

            try
            {
                conn.Open();
                using (OdbcCommand cmd = new OdbcCommand(req, conn))
                {
                    cmd.Parameters.AddWithValue("@Value", value);
                    OdbcDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    List<T> list = MapList(dt, table);
                    return list;
                }
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        private static List<T> MapList<T>(DataTable dt, T c)
        {
            List<T> list = new List<T>();

            PropertyInfo[] propertyInfoObject = c.GetType().GetProperties();
            T t = Activator.CreateInstance<T>();

            foreach (DataRow dr in dt.Rows)
            {

                foreach (PropertyInfo fi in propertyInfoObject)
                {
                    fi.SetValue(t, dr[fi.Name]);
                }


                list.Add(t);

            }

            return list;
        }

        public static MappingObject GetTypeOfPro<T>(T c)
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




    }
}
