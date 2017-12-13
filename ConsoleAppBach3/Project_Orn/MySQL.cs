using System;
using System.Collections;
using System.Collections.Generic;
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


        public static MappingObject GetTypeOfPro<T>(T c)
        {
            MappingObject mappingObject = new MappingObject();
            Console.WriteLine(c.GetType().Name);

            mappingObject.ObjectName = c.GetType().ToString();
            foreach (PropertyInfo propertyInfoObject in c.GetType().GetProperties())
            {

                switch (propertyInfoObject.PropertyType.Name.ToString())
                {
                    case "Int32":
                        {
                            mappingObject.PropertyInfos.Add(key: propertyInfoObject.Name, value: "INT");
                            break;
                        }
                    case "String":
                        {
                            mappingObject.PropertyInfos.Add(key: propertyInfoObject.Name, value: "MEDIUMTEXT");
                            break;
                        }
                    case "DateTime":
                        {
                            mappingObject.PropertyInfos.Add(key: propertyInfoObject.Name, value: "DATETIME");
                            break;
                        }
                    case "Boolean":
                        {
                            mappingObject.PropertyInfos.Add(key: propertyInfoObject.Name, value: "BIT");
                            break;
                        }
                    case "Single":
                        {
                            mappingObject.PropertyInfos.Add(key: propertyInfoObject.Name, value: "FLOAT");
                            break;
                        }
                    case "Double":
                        {
                            mappingObject.PropertyInfos.Add(key: propertyInfoObject.Name, value: "DOUBLE");
                            break;
                        }
                }
                //String propertyName = propertyInfoObject.Name;
                //Console.WriteLine("La valeur est {0}",propertyName);
                //String propertytype = propertyInfoObject.PropertyType.Name.ToString();
                // Console.WriteLine("La valeur est {0}", propertyInfoObject.PropertyType.Name.ToString());
            }

            return mappingObject;

        }

    }
}
