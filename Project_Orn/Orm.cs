using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;

namespace Project_Orn
{
    public  static class Orm
    {

        public static void testSqlServer()
        {
            // Instantiate the connection
            SqlConnection conn = new SqlConnection("Data Source=(local);Initial Catalog=orm;User ID=thomas;Password=thomas");
    
            SqlDataReader rdr = null;

            try
            {
                // Open the connection
                conn.Open();

                // Informe the user if it's a succed
                Console.WriteLine("Connection établie, que souhaitez-vous faire ? \n 1 - Lire la base de donnée \n 2 - Insérer une valeure \n 3 - Modifier une valeure");

                string choix = Console.ReadLine();
                switch (choix.ToLower())
                {
                    case "1":
                        Console.WriteLine("Affichage complet :");
                        
                        // Pass the connection to a command object
                        SqlCommand cmd = new SqlCommand("select * from ormTable", conn);

                        // get query results
                        rdr = cmd.ExecuteReader();

                        // print the CustomerID of each record
                        while (rdr.Read())
                        {
                            Console.WriteLine(rdr[1]);
                        }

                        break;
                    case "2":
                        Console.WriteLine("Insérer");
                        insertSqlServer(conn);
                        break;
                    case "3":
                        Console.WriteLine("Modifier");
                        updateSqlServer(conn);
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

        
        public static void insertSqlServer(SqlConnection conn)
        {
            Console.WriteLine("INSERT :");
            Console.WriteLine("Quelle nom voulez-vous insérer ?");
            String name = Console.ReadLine();

            string insertString = @"insert into ormTable (name) values (@name)";

            SqlCommand cmd = new SqlCommand(insertString, conn);

            cmd.Parameters.AddWithValue("@name", name);

            cmd.ExecuteNonQuery();

            Console.Read();
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



        /*
        public static void testOracle()
        {
            OdbcConnection conn = new OdbcConnection("DSN=SAMPLE_ISAM");

 
            // 1. Instantiate the connection
          


            OdbcDataReader rdr = null;

            try
            {
                // 2. Open the connection
                conn.Open();

                // 3. Pass the connection to a command object
                OdbcCommand cmd = new OdbcCommand("select * from Customerss", conn);

                //
                // 4. Use the connection
                //

                // get query results
                rdr = cmd.ExecuteReader();

                // print the CustomerID of each record
                while (rdr.Read())
                {
                    Console.WriteLine(rdr);
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
        }*/
    }
}

