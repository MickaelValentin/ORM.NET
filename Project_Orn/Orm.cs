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

        public static void sqlServer()
        {
            var sql = new SQL();
            sql.connection();
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

