using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Project_Orn
{
    public static class PostGreMapping
    {
        public static void Select()
        {

            // 1. Instantiate the connection
            OdbcConnection conn = new OdbcConnection(
                "Driver={PostgreSQL ODBC Driver(UNICODE)};Server=localhost;Port=5432;Database=orm;UID=mickaël;PWD=170514");
            // 1. Instantiate the connection


            OdbcDataReader rdr = null;

            try
            {
                // 2. Open the connection
                conn.Open();

                // 3. Pass the connection to a command object
                OdbcCommand cmd = new OdbcCommand("select * from customers", conn);

                //
                // 4. Use the connection
                //

                // get query results
                rdr = cmd.ExecuteReader();

                // print the CustomerID of each record
                while (rdr.Read())
                {
                    Console.WriteLine(rdr["name"]);
                }
                Console.ReadLine();

            }
            finally
            {
                // close the reader
                if (rdr != null)
                {
                    rdr.Close();
                }

                // 5. Close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public static void Insert()
        {
            Console.WriteLine("Que voulez-vous insérer ?");
            Console.WriteLine("Entrez le nom de la propriété");
            string propertyname = Console.ReadLine();


            // 1. Instantiate the connection
            OdbcConnection conn = new OdbcConnection(
                "Driver={PostgreSQL ODBC Driver(UNICODE)};Server=localhost;Port=5432;Database=orm;UID=mickaël;PWD=170514");


            try
            {
                // 2. Open the connection
                conn.Open();

                // 3. Pass the connection to a command object
                OdbcCommand cmd = new OdbcCommand("insert into customers2 (surname) values (?)", conn);


                cmd.Parameters.Add("@PropertyName", OdbcType.NVarChar).Value = propertyname;


                cmd.ExecuteNonQuery();



            }
            finally
            {

                // 5. Close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            }

        }

        public static void Update()
        {
            Console.WriteLine("Que voulez-vous modifier ?");
            Console.WriteLine("Entrez la valeur à modifier");
            string oldvalue = Console.ReadLine();
            Console.WriteLine("Entrez la nouvelle valeur");
            string newvalue = Console.ReadLine();


            // 1. Instantiate the connection
            OdbcConnection conn = new OdbcConnection(
                "Driver={PostgreSQL ODBC Driver(UNICODE)};Server=localhost;Port=5432;Database=orm;UID=mickaël;PWD=170514");


            try
            {
                // 2. Open the connection
                conn.Open();

                // 3. Pass the connection to a command object
                OdbcCommand cmd = new OdbcCommand("update customers set prenom  = ? where prenom = ?", conn);


                cmd.Parameters.Add("@NewValue", OdbcType.NVarChar).Value = newvalue;
                cmd.Parameters.Add("@OldValue", OdbcType.NVarChar).Value = oldvalue;

                cmd.ExecuteNonQuery();



            }
            finally
            {

                // 5. Close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            }

        }

        public static void Delete()
        {
            Console.WriteLine("Que voulez-vous supprimer ?");
            Console.WriteLine("Entrez le nom de la propriété");
            string propertyname = Console.ReadLine();


            // 1. Instantiate the connection
            OdbcConnection conn = new OdbcConnection(
                "Driver={PostgreSQL ODBC Driver(UNICODE)};Server=localhost;Port=5432;Database=orm;UID=mickaël;PWD=170514");


            try
            {
                // 2. Open the connection
                conn.Open();

                // 3. Pass the connection to a command object
                OdbcCommand cmd = new OdbcCommand("delete from customers2 where surname = ?", conn);

                cmd.Parameters.Add("@PropertyName", OdbcType.NVarChar).Value = propertyname;


                cmd.ExecuteNonQuery();



            }
            finally
            {

                // 5. Close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            }

        }

        public static void CreateTable()
        {
            Console.WriteLine("Entrez le nom de la table à créer");
            string tablename = Console.ReadLine();
            Console.WriteLine("Entrez le nom de la colonne à créer");
            string columnname = Console.ReadLine();
            Console.WriteLine("Entrez le type de donnée de la colonne");
            string columntype = Console.ReadLine();


            // 1. Instantiate the connection
            OdbcConnection conn = new OdbcConnection(
                "Driver={PostgreSQL Unicode ;Server=localhost;Port=5432;Database=orm;UID=mickaël;PWD=170514");


            try
            {
                // 2. Open the connection
                conn.Open();

                // 3. Pass the connection to a command object
                OdbcCommand cmd = new OdbcCommand("create table ? ( ? ? )", conn);

                cmd.Parameters.Add("@TableName", OdbcType.NVarChar).Value = tablename;
                cmd.Parameters.Add("@ColumnName", OdbcType.NVarChar).Value = columnname;
                cmd.Parameters.Add("@ColumnType", OdbcType.NVarChar).Value = columntype;


                cmd.ExecuteNonQuery();



            }
            finally
            {

                // 5. Close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            }

        }

        public static void DropTable()
        {
            Console.WriteLine("Entrez le nom de la table à supprimer");
            string tablename = Console.ReadLine();


            // 1. Instantiate the connection
            OdbcConnection conn = new OdbcConnection(
                "Driver={PostgreSQL ODBC Driver(UNICODE)};Server=localhost;Port=5432;Database=orm;UID=mickaël;PWD=170514");


            try
            {
                // 2. Open the connection
                conn.Open();

                // 3. Pass the connection to a command object
                OdbcCommand cmd = new OdbcCommand("DROP TABLE IF EXISTS ?", conn);

                cmd.Parameters.Add("@TableName", OdbcType.NVarChar).Value = tablename;



                cmd.ExecuteNonQuery();



            }
            finally
            {

                // 5. Close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            }

        }
        public static bool InsertNextGen<T>(T obj)
        {
            MappingObject objectMapping = new MappingObject();
            objectMapping = MappingOperations.GetTypeOfProPostGre(obj);
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
                using (OdbcConnection conn = GetConnection("PostgreSQL UNICODE)", "localhost", "5432",
   "test", "postgres", "root"))
                {
                    conn.Open();
                    using (OdbcCommand qureyToInsert = new OdbcCommand(reqInsertElement, conn))
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

        public static bool CreateTableNextGen<T>(T obj)
        {
            MappingObject objectMapping = new MappingObject();
            objectMapping = MappingOperations.GetTypeOfProPostGre(obj);


            string reqCreateTable = $"CREATE TABLE IF NOT EXISTS {objectMapping.ObjectName}(ID SERIAL PRIMARY KEY,";
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

                using (OdbcConnection conn = GetConnection("PostgreSQL Unicode", "localhost", "5432",
                   "testorm", "postgres", "root"))
                {
                    conn.Open();
                    using (OdbcCommand qureyToCreateTable = new OdbcCommand(reqCreateTable, conn))
                    {
                        qureyToCreateTable.ExecuteNonQuery();
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

        public static List<T> SelectTableNextGen<T>(string column, string value, T table)
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
                using (OdbcConnection conn = GetConnection("PostgreSQL ODBC Driver(UNICODE)", "localhost", "5432",
                             "test", "root", "root"))
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

        public static bool DeleteElemetFromTableNextGen<T>(string column, string value, T table)
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
                using (OdbcConnection conn = GetConnection("PostgreSQL ODBC Driver(UNICODE)", "localhost", "5432",
        "test", "root", "root"))
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

        public static bool DropTableNextGen<T>(T obj)
        {
            MappingObject objectMapping = new MappingObject();
            objectMapping = MappingOperations.GetTypeOfProPostGre(obj);
            string reqDropTable = $"DROP TABLE IF EXISTS {objectMapping.ObjectName}";
            try
            {
                using (OdbcConnection conn = GetConnection("PostgreSQL ODBC Driver(UNICODE)", "localhost", "5432",
                   "test", "root", "root"))
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

        public static bool UpdateElementNextGen<T>(int id, T table)
        {
            if (table.GetType().Name == null)
            {
                Console.WriteLine("obj not found");
                return false;

            }
            MappingObject objectMapping = new MappingObject();
            objectMapping = MappingOperations.GetTypeOfProPostGre(table);
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
                using (OdbcConnection conn = GetConnection("PostgreSQL ODBC Driver(UNICODE)", "localhost", "5432",
                 "test", "postgres", "root"))
                {
                    conn.Open();
                    using (OdbcCommand qureyUpdate = new OdbcCommand(reqUpdate, conn))
                    {
                        for (int i = 0; i < objectMapping.PropertiesAttributes.Count(); i++)
                        {
                            PropertyAttributes infoFormapping = objectMapping.PropertiesAttributes[i];
                            qureyUpdate.Parameters.AddWithValue($"{infoFormapping.NameInfo}", infoFormapping.ValueInfo);
                        }
                        qureyUpdate.Parameters.AddWithValue($"id", id);
                        qureyUpdate.Prepare();
                        qureyUpdate.ExecuteNonQuery();
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

        private static OdbcConnection GetConnection(string driver, string server, string port,
            string database, string user, string password)
        {
            return new OdbcConnection(
                $"DRIVER={{{driver}}};" +
                $"SERVER={server};" +
                $"PORT={port};" +
                $"DATABASE={database};" +
                $"Uid={user};" +
                $"Pwd={password}");
        }
    }

}