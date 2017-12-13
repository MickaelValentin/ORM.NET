using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;

namespace Project_Orn
{
    public class PostGre
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
            catch (Exception e)
            {
                Console.WriteLine($"Erreur : {e.Message}");
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
            catch (Exception e)
            {
                Console.WriteLine($"Erreur : {e.Message}");
                Console.ReadLine();
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
            catch (Exception e)
            {
                Console.WriteLine($"Erreur : {e.Message}");
                Console.ReadLine();
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
            catch (Exception e)
            {
                Console.WriteLine($"Erreur : {e.Message}");
                Console.ReadLine();
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
                "Driver={PostgreSQL ODBC Driver(UNICODE)};Server=localhost;Port=5432;Database=orm;UID=mickaël;PWD=170514");

            string req = $"create table {tablename} ( {columnname} {columntype} )";
            try
            {
                // 2. Open the connection
                conn.Open();

                // 3. Pass the connection to a command object
                OdbcCommand cmd = new OdbcCommand(req, conn);


                cmd.ExecuteNonQuery();



            }
            catch (Exception e)
            {
                Console.WriteLine($"Erreur : {e.Message}");
                Console.ReadLine();
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

            string req = $"DROP TABLE IF EXISTS {tablename}";
            try
            {
                // 2. Open the connection
                conn.Open();
              
                // 3. Pass the connection to a command object
                OdbcCommand cmd = new OdbcCommand(req, conn);
               
                cmd.ExecuteNonQuery();



            }
            catch (Exception e)
            {
                Console.WriteLine($"Erreur : {e.Message}");
                Console.ReadLine();
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
    }
}
