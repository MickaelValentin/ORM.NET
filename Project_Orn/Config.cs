using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Reflection;

namespace Project_Orn
{
    public  static class Config
    {

        public static void testSqlServer()
        {


            // 1. Instantiate the connection
            SqlConnection conn = new SqlConnection(
                "Data Source=(local);Initial Catalog=project;User ID=sa;Password=q");
            // 1. Instantiate the connection


            SqlDataReader rdr = null;

            try
            {
                // 2. Open the connection
                conn.Open();

                // 3. Pass the connection to a command object
                SqlCommand cmd = new SqlCommand("select * from Customerss", conn);

                //
                // 4. Use the connection
                //

                // get query results
                rdr = cmd.ExecuteReader();

                // print the CustomerID of each record
                while (rdr.Read())
                {
                    Console.WriteLine(rdr[1]);
                }
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

        public static void testOracle()
        {

            //    OdbcConnection conn = new OdbcConnection("DSN=DRIVER={MySQL ODBC 3.51 Driver;SERVER=localhost;DATABASE=test;UID=root;PWD=root; OPTION=3");




            OdbcConnection conn = new OdbcConnection(
 

            "DRIVER={MySQL ODBC 5.3 ANSI Driver};" +
                   "SERVER=localhost;" +
                   "DATABASE=test;" +
                   "USER=root;" +
                   "PASSWORD=root;" +
                   "OPTION=3;");


            // 1. Instantiate the connection



            OdbcDataReader rdr = null;

            try
            {
                // 2. Open the connection
                conn.Open();

                // 3. Pass the connection to a command object
                OdbcCommand cmd = new OdbcCommand("select * from orm", conn);

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
                Console.ReadKey();
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

        public static void testPosgreSql()
        {

            //    OdbcConnection conn = new OdbcConnection("DSN=DRIVER={MySQL ODBC 3.51 Driver;SERVER=localhost;DATABASE=test;UID=root;PWD=root; OPTION=3");




            OdbcConnection conn = new OdbcConnection(

            "DRIVER={PostgreSQL UNICODE};" +
                   "SERVER=localhost;" +
                    "Port=5432;" +
                   "database=testorm;" +  
                   "UID=postgres;" +
                   "PWD=root;");


            // 1. Instantiate the connection



            OdbcDataReader rdr = null;

            try
            {
                // 2. Open the connection
                conn.Open();

                // 3. Pass the connection to a command object
                OdbcCommand cmd = new OdbcCommand("select * from test", conn);

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
                Console.ReadKey();
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

        public static void GetTypeOfPro<T>(T c)
        {

            Console.WriteLine(c.GetType());
            foreach (PropertyInfo propertyInfo in c.GetType().GetProperties())
            {
              
                Console.WriteLine("Name property {0}", propertyInfo.Name);
                Console.WriteLine("Type property {0}", propertyInfo.PropertyType);
            }
            Console.ReadKey();



        }

    }

    public static class Configuration
    {
        public static void connection()
        {
            SqlConnection conn = new SqlConnection("Data Source = (local); Initial Catalog = project; User ID = dinesh; Password=hello");
            conn.Open();
            SqlDataReader rdr = null;

            try
            {
                // 2. Open the connection
                conn.Open();

                // 3. Pass the connection to a command object
                SqlCommand cmd = new SqlCommand("select * from Customerss", conn);

                //
                // 4. Use the connection
                //

                // get query results
                rdr = cmd.ExecuteReader();

                // print the CustomerID of each record
                while (rdr.Read())
                {
                    Console.WriteLine(rdr[1]);
                }
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

        static void WriteColumnMappings<Contact>(Contact item) where Contact : new()
        {
            // Just grabbing this to get hold of the type name:
            var type = item.GetType();

            // Get the PropertyInfo object:
            var properties = item.GetType().GetProperties();
            Console.WriteLine("Finding properties for {0} ...", type.Name);
            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(false);
                string msg = "the {0} property maps to the {1} database column";
                var columnMapping = attributes
                    .FirstOrDefault(a => a.GetType() == typeof(DbColumnAttribute));
                if (columnMapping != null)
                {
                    var mapsto = columnMapping as DbColumnAttribute;
                    Console.WriteLine(msg, property.Name, mapsto.Name);
                }
            }
        }

 
    }
    public class DbColumnAttribute : Attribute
    {
        public string Name { get;  set; }
        public DbColumnAttribute(string name)
        {
            this.Name = name;
        }
    }
}
 

