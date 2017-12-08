using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrnProject;
using System.Data.SqlClient;
using System.Data.Odbc;


namespace OrnProject
{
    class CSD_ODB
    {
        public static void createSqlTable()
        {
            OdbcConnection conn = new OdbcConnection(


            "DRIVER={MySQL ODBC 5.3 ANSI Driver};" +
                   "SERVER=localhost;" +
                   "DATABASE=orm;" +
                   "USER=root;");

            /*Console.WriteLine("Enter your table name");
            string nomTable = Console.ReadLine();
            Console.WriteLine($"Entrer le nombre de colonnes de la table {nomTable}");
            string nombreColonne = Console.ReadLine();
            int nombre = Int32.Parse(nombreColonne);
            string nomCol;
            string nomAllCol = "";
            string type;

            for (int i = 0; i < nombre; i++)
            {
                Console.WriteLine($"Nom Colonne {i + 1} :");
                nomCol = Console.ReadLine();

                Console.WriteLine("Type de la colonne :");
                type = Console.ReadLine();
                type.ToLower();

                if (i == 0)
                {
                    nomAllCol = nomCol;
                }
                else
                {
                    nomAllCol = $"{nomAllCol},{nomCol} {type}";
                }

            }*/

            try
            {
                // 2. Open the connection
                conn.Open();

                //OdbcCommand cmd = new OdbcCommand($"CREATE TABLE {nomTable}()", conn);
                OdbcCommand cmd = new OdbcCommand($"CREATE TABLE testcreate2(Name char(50))", conn);
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

            try
            {
                // 2. Open the connection
                conn.Open();

                //OdbcCommand cmd = new OdbcCommand($"CREATE TABLE {nomTable}()", conn);
                OdbcCommand cmd = new OdbcCommand($"SELECT * FROM test", conn);
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

            try
            {
                // 2. Open the connection
                conn.Open();

                OdbcCommand cmd = new OdbcCommand("DELETE FROM contact", conn);
                cmd.ExecuteNonQuery();

                Console.WriteLine("La table à bien été suprimer");
            }

            finally
            {


                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
    }
}


