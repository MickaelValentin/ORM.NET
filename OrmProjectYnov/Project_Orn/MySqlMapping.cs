using System;
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
    public static class MySqlMapping
    {
        #region Old Code
        /*
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

                
e.WriteLine("La table à bien été créer");
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
        */
        #endregion
        public static bool InsertNextGen<T>(ConnectionMySql connection, T obj)
        {
            MappingObject objectMapping = new MappingObject();
            objectMapping = MappingOperations.GetTypeOfProMySQL(obj);
            string reqInsertElement = $" INSERT INTO {objectMapping.ObjectName} VALUES(NULL,";
            for (int i = 0; i < objectMapping.PropertiesAttributes.Count(); i++)
            {
                if (i == objectMapping.PropertiesAttributes.Count() - 1)
                {
                    reqInsertElement += "?";
                }
                else
                {
                    reqInsertElement += "?,";
                }
            }
            reqInsertElement += ")";

            try
            {
                using (OdbcConnection conn = GetConnection(connection.Driver, connection.Server,
                    connection.DataBase, connection.User, connection.Password))

                {
                    conn.Open();
                    using (OdbcCommand queryToInsert = new OdbcCommand(reqInsertElement, conn))
                    {
                        for (int i = 0; i < objectMapping.PropertiesAttributes.Count(); i++)
                        {
                            PropertyAttributes infoFormapping = objectMapping.PropertiesAttributes[i];
                            queryToInsert.Parameters.AddWithValue($"{infoFormapping.NameInfo}", infoFormapping.ValueInfo);
                        }
                        queryToInsert.Prepare();
                        queryToInsert.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public static bool CreateTableNextGen<T>(ConnectionMySql connection, T obj)
        {
            MappingObject objectMapping = new MappingObject();
            objectMapping = MappingOperations.GetTypeOfProMySQL(obj);
            string reqCreateTable = $"CREATE TABLE IF NOT EXISTS {objectMapping.ObjectName}(ID int NOT NULL AUTO_INCREMENT,";
            for (int i = 0; i < objectMapping.PropertiesAttributes.Count(); i++)
            {
                reqCreateTable += $"{objectMapping.PropertiesAttributes[i].NameInfo} {objectMapping.PropertiesAttributes[i].TypeInfo},";
            }
            reqCreateTable += "PRIMARY KEY(ID))";
            try
            {
                using (OdbcConnection conn = GetConnection(connection.Driver, connection.Server,
                  connection.DataBase, connection.User, connection.Password))
                {
                    conn.Open();
                    using (OdbcCommand queryToCreateTable = new OdbcCommand(reqCreateTable, conn))
                    {
                        queryToCreateTable.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public static List<T> SelectTableNextGen<T>(ConnectionMySql connection, string column, string value, T table)
        {
            string reqSelectElement;
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
                reqSelectElement = $"SELECT * FROM {table.GetType().Name.ToString()}";
            }
            else
            {
                reqSelectElement = $"SELECT * FROM {table.GetType().Name.ToString()} WHERE {column} = ?";
            }

            try
            {
                using (OdbcConnection conn = GetConnection(connection.Driver, connection.Server,
     connection.DataBase, connection.User, connection.Password))
                {
                    conn.Open();
                    using (OdbcCommand queryToSelectElement = new OdbcCommand(reqSelectElement, conn))
                    {
                        queryToSelectElement.Parameters.AddWithValue(column, value);
                        queryToSelectElement.Prepare();
                        OdbcDataReader dr = queryToSelectElement.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr);
                        List<T> list = MappingOperations.MapList(dt, table);
                        return list;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public static bool DeleteElemetFromTableNextGen<T>(ConnectionMySql connection, string column, string value, T table)
        {
            if (table.GetType().Name == null)
            {
                Console.WriteLine("obj not found");
                return false;
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
                return false;
            }
            string reqDelete = $"DELETE FROM {table.GetType().Name.ToString()} WHERE {column} = ?";
            try
            {
                using (OdbcConnection conn = GetConnection(connection.Driver, connection.Server,
            connection.DataBase, connection.User, connection.Password))
                {
                    conn.Open();
                    using (OdbcCommand queryToDeleteElement = new OdbcCommand(reqDelete, conn))
                    {
                        queryToDeleteElement.Parameters.AddWithValue(column, value);
                        queryToDeleteElement.Prepare();
                        queryToDeleteElement.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public static bool DropTableNextGen<T>(ConnectionMySql connection, T obj)
        {
            MappingObject objectMapping = new MappingObject();
            objectMapping = MappingOperations.GetTypeOfProMySQL(obj);
            string reqDropTable = $"DROP TABLE IF EXISTS {objectMapping.ObjectName}";
            try
            {
                using (OdbcConnection conn = GetConnection(connection.Driver, connection.Server,
                   connection.DataBase, connection.User, connection.Password))
                {
                    conn.Open();

                    using (OdbcCommand queryToDropTable = new OdbcCommand(reqDropTable, conn))
                    {
                        queryToDropTable.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception c)
            {
                Console.WriteLine(c);
                return false;

            }
        }

        public static bool UpdateElementNextGen<T>(ConnectionMySql connection, int id, T table)
        {
            if (table.GetType().Name == null)
            {
                Console.WriteLine("obj not found");
                return false;

            }
            MappingObject objectMapping = new MappingObject();
            objectMapping = MappingOperations.GetTypeOfProMySQL(table);
            string reqUpdate = $"UPDATE  {table.GetType().Name.ToString()} SET ";
            for (int i = 0; i < objectMapping.PropertiesAttributes.Count(); i++)
            {
                reqUpdate += $"{objectMapping.PropertiesAttributes[i].NameInfo}= ?";
                if (i != objectMapping.PropertiesAttributes.Count() - 1)
                {
                    reqUpdate += ",";
                }
            }
            reqUpdate += $" WHERE id = ?";
            try
            {
                using (OdbcConnection conn = GetConnection(connection.Driver, connection.Server,
                 connection.DataBase, connection.User, connection.Password))
                {
                    conn.Open();
                    using (OdbcCommand queryUpdate = new OdbcCommand(reqUpdate, conn))
                    {
                        for (int i = 0; i < objectMapping.PropertiesAttributes.Count(); i++)
                        {
                            PropertyAttributes infoFormapping = objectMapping.PropertiesAttributes[i];
                            queryUpdate.Parameters.AddWithValue($"{infoFormapping.NameInfo}", infoFormapping.ValueInfo);
                        }
                        queryUpdate.Parameters.AddWithValue($"id", id);
                        queryUpdate.Prepare();
                        queryUpdate.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public static OdbcConnection GetConnection(string driver, string server,
            string database, string user, string password)
        {
            return new OdbcConnection(
                $"DRIVER={{{driver}}};" +
                $"SERVER={server};" +
                $"DATABASE={database};" +
                $"USER={user};" +
                $"PASSWORD={password}");
        }






    }
}

