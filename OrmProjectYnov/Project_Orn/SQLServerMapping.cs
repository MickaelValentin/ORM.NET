using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Orn
{
   public class SQLServerMapping
    {
        #region OldCode
        /*
        public void Connection()
        {
            // Instantiate the connection
            SqlConnection conn = new SqlConnection("Data Source=(local);Initial Catalog=orm;User ID=thomas;Password=thomas");

            SqlDataReader rdr = null;

            try
            {
                // Open the connection
                conn.Open();

                // Informe the user if it's a succed
                Console.WriteLine("Connection établie, que souhaitez-vous faire ? \n 1 - Lire la base de donnée \n 2 - Insérer une valeure \n 3 - Modifier une valeure \n 4 - Supprimer une valeure\n 5 - Créer une table\n 6 - Suppimer une table");

                string choix = Console.ReadLine();
                switch (choix.ToLower())
                {
                    case "1":
                        readSqlServer(conn, rdr);
                        break;
                    case "2":
                        insertSqlServer(conn);
                        break;
                    case "3":
                        updateSqlServer(conn);
                        break;
                    case "4":
                        deleteSqlServer(conn);
                        break;
                    case "5":
                        createTableSqlServer(conn);
                        break;
                    case "6":
                        deleteTableSqlServer(conn);
                        break;
                    default:
                        Console.WriteLine("Erreur dans le choix");
                        break;
                }


            }
            catch (Exception e)
            {
                Console.WriteLine($"Erreur : {e.Message}");
            }
            finally
            {
                // Close the reader
                if (rdr != null)
                {
                    rdr.Close();
                }

                // Close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        private void createTableSqlServer(SqlConnection conn)
        {
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText =
                @" 
                        BEGIN
                        CREATE TABLE ormTableCreated (
                          Id    integer PRIMARY KEY NOT NULL,
                          Name  varchar(200) NOT NULL
                        ); 
                    END";
            cmd.ExecuteNonQuery();

            Console.WriteLine("Table créée");
        }

        private void deleteTableSqlServer(SqlConnection conn)
        {
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText =
                @" 
                    BEGIN
                    DROP TABLE ormTableCreated; 
                END";
            cmd.ExecuteNonQuery();

            Console.WriteLine("Table supprimée");
        }

        private static void readSqlServer(SqlConnection conn, SqlDataReader rdr)
        {
            // Pass the connection to a command object
            SqlCommand cmd = new SqlCommand("select * from ormTable", conn);

            // get query results
            rdr = cmd.ExecuteReader();

            // print the CustomerID of each record
            while (rdr.Read())
            {
                Console.WriteLine(rdr[1]);
            }

        }

        public static void insertSqlServer(SqlConnection conn)
        {
            Console.WriteLine("INSERT :");
            Console.WriteLine("Quelle nom voulez-vous insérer ?");
            String name = Console.ReadLine();

            string insertString = @"insert into ormTable (name) values (@name)";

            SqlCommand cmd = new SqlCommand(insertString, conn);

            cmd.Parameters.AddWithValue("@name", name);

            cmd.ExecuteNonQuery();
        }

        public static void updateSqlServer(SqlConnection conn)
        {
            Console.WriteLine("UPDATE :");
            Console.WriteLine("Quelle nom voulez-vous mofidier ?");
            String currentName = Console.ReadLine();
            Console.WriteLine("Que rentrer à la place ?");
            String newName = Console.ReadLine();

            string updateString = @" update ormTable set name = @newName where name = @currentName";

            SqlCommand cmd = new SqlCommand(updateString);

            cmd.Parameters.AddWithValue("@currentName", currentName);
            cmd.Parameters.AddWithValue("@newName", newName);

            cmd.Connection = conn;

            cmd.ExecuteNonQuery();
        }

        public static void deleteSqlServer(SqlConnection conn)
        {
            Console.WriteLine("DELETE :");
            Console.WriteLine("Quelle nom voulez-vous supprimer ?");
            String deleteName = Console.ReadLine();

            string deleteString = @" delete from ormTable where name = @deleteName";

            SqlCommand cmd = new SqlCommand();

            cmd.Parameters.AddWithValue("@deleteName", deleteName);

            cmd.CommandText = deleteString;

            cmd.Connection = conn;

            cmd.ExecuteNonQuery();

        }
         */
        #endregion

        public static SqlConnection GetConnection(string server, string database, string user, string password)
        {
            return new SqlConnection(
                $"Data Source={server};" +
                $"Initial Catalog={database};" +
                $"User ID={user};" +
                $"Password={password}");
        }

        public static bool CreateTableNextGen<T>(SqlConnection connection,T obj)
        {
            MappingObject objectMapping = new MappingObject();
            objectMapping = MappingOperations.GetTypeOfProSQLServer(obj);

            string reqCreateTable = $"CREATE TABLE  {objectMapping.ObjectName}(ID INT PRIMARY KEY,";
            for (int i = 0; i < objectMapping.PropertiesAttributes.Count(); i++)
            {
                reqCreateTable += $"{objectMapping.PropertiesAttributes[i].NameInfo} {objectMapping.PropertiesAttributes[i].TypeInfo}";
                if (i != objectMapping.PropertiesAttributes.Count() - 1)
                {
                    reqCreateTable += ",";
                }
            }

            reqCreateTable += ")";
            try
            {
                using (SqlConnection conn = connection)
                {
                    conn.Open();
                    using ( SqlCommand qureyToCreateTable = new SqlCommand(reqCreateTable, conn))
                    {
                        qureyToCreateTable.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
                return false;
            }
        }

        public static bool InsertNextGen<T>(SqlConnection connection,T obj)
        {
            MappingObject objectMapping = new MappingObject();
            objectMapping = MappingOperations.GetTypeOfProSQLServer(obj);
            string reqInsertElement = $" INSERT INTO {objectMapping.ObjectName} VALUES(DEFAULT,";
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
                using (SqlConnection conn = connection)
                {
                    conn.Open();
                    using (SqlCommand qureyToInsert = new SqlCommand(reqInsertElement, conn))
                    {
                        for (int i = 0; i < objectMapping.PropertiesAttributes.Count(); i++)
                        {
                            PropertyAttributes infoFormapping = objectMapping.PropertiesAttributes[i];
                            qureyToInsert.Parameters.AddWithValue($"{infoFormapping.NameInfo}", infoFormapping.ValueInfo);
                        }
                        qureyToInsert.Prepare();
                        qureyToInsert.ExecuteNonQuery();
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

    }
}
