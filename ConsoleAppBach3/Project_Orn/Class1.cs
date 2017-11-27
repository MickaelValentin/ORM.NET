﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;

namespace Project_Orn
{
    public  static class Class1
    {
        

        // 1. Instantiate the connection

        public static void testSqlServer()
        {


            // 1. Instantiate the connection
            SqlConnection conn = new SqlConnection(
                "Data Source=(local);Initial Catalog=orm;User ID=mickaël;Password=170514");
            // 1. Instantiate the connection


            SqlDataReader rdr = null;

            try
            {
                // 2. Open the connection
                conn.Open();

                // 3. Pass the connection to a command object
                SqlCommand cmd = new SqlCommand("select * from Customers", conn);

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

        public static void PostGreConnexion()
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

        public static void InsertPostGre()
        {
            Console.WriteLine("Que voulez-vous insérer ?");
            /*Console.WriteLine("Entrez le nom de la table");
            string tablename = Console.ReadLine();*/
            /*Console.WriteLine("Entrez le nom de la colonne");
            string columnname = Console.ReadLine();*/
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

                // 2. define parameters used in command object
                /*OdbcParameter param1 = new OdbcParameter();
                param1.ParameterName = "@TableName";
                param1.Value = tablename;*/

                /*OdbcParameter param2 = new OdbcParameter(); 
                param2.ParameterName = "@ColumnName";
                param2.Value = columnname;*/

                /* OdbcParameter param3 = new OdbcParameter();
                 param3.ParameterName = "@PropertyName";
                 param3.Value = propertyname;*/

                // 3. add new parameter to command object
                //cmd.Parameters.Add(param1);
                //cmd.Parameters.Add(param2);
                //cmd.Parameters.Add(param3);

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

        public static void UpdatePostGre()
        {
            Console.WriteLine("Que voulez-vous modifier ?");
            /*Console.WriteLine("Entrez le nom de la table");
            string tablename = Console.ReadLine();*/
            /*Console.WriteLine("Entrez le nom de la colonne");
            string columnname = Console.ReadLine();*/
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

                // 2. define parameters used in command object
                /*OdbcParameter param1 = new OdbcParameter();
                param1.ParameterName = "@TableName";
                param1.Value = tablename;*/

                /*OdbcParameter param2 = new OdbcParameter(); 
                param2.ParameterName = "@ColumnName";
                param2.Value = columnname;*/

                /*OdbcParameter param3 = new OdbcParameter();
                param3.ParameterName = "@NewValue";
                param3.Value = newvalue;*/

                /*OdbcParameter param4 = new OdbcParameter();
                param4.ParameterName = "@OldValue";
                param4.Value = oldvalue;*/

                // 3. add new parameter to command object
                //cmd.Parameters.Add(param1);
                //cmd.Parameters.Add(param2);
                //cmd.Parameters.Add(param3);
                //cmd.Parameters.Add(param4);


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

        public static void DeleteSQLServer()
        {
            /*Console.WriteLine("Entrez le nom de la table");
            string tablename = Console.ReadLine();*/
            /*Console.WriteLine("Entrez le nom de la colonne");
            string columnname = Console.ReadLine();*/
            Console.WriteLine("Entrez le nom de la propriété");
            string propertyname = Console.ReadLine();

            
            // 1. Instantiate the connection
            SqlConnection conn = new SqlConnection(
                "Data Source=(local);Initial Catalog=orm;User ID=mickaël;Password=170514");
            

            try
            {
                // 2. Open the connection
                conn.Open();

                // 3. Pass the connection to a command object
                SqlCommand cmd = new SqlCommand("delete from Customers where surname = @PropertyName", conn);

                // 2. define parameters used in command object
                /*SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@TableName";
                param1.Value = tablename;*/

                /*SqlParameter param2 = new SqlParameter();
                param2.ParameterName = "@ColumnName";
                param2.Value = columnname;*/

                SqlParameter param3 = new SqlParameter();
                param3.ParameterName = "@PropertyName";
                param3.Value = propertyname;

                // 3. add new parameter to command object
                //cmd.Parameters.Add(param1);
                //cmd.Parameters.Add(param2);
                cmd.Parameters.Add(param3);


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


        public static void DeleteMySQL()
        {
            //    OdbcConnection conn = new OdbcConnection("DSN=DRIVER={MySQL ODBC 3.51 Driver;SERVER=localhost;DATABASE=test;UID=root;PWD=root; OPTION=3");


            /*Console.WriteLine("Entrez le nom de la table");
            string tablename = Console.ReadLine();*/
            /*Console.WriteLine("Entrez le nom de la colonne");
            string columnname = Console.ReadLine();*/
            Console.WriteLine("Entrez le nom de la propriété");
            string propertyname = Console.ReadLine();

            OdbcConnection conn = new OdbcConnection(


            "DRIVER={MySQL ODBC 5.3 ANSI Driver};" +
                   "SERVER=localhost;" +
                   "DATABASE=test;" +
                   "USER=root;" +
                   "PASSWORD=root;" +
                   "OPTION=3;");


            // 1. Instantiate the connection

            try
            {
                // 2. Open the connection
                conn.Open();

                // 3. Pass the connection to a command object
                OdbcCommand cmd = new OdbcCommand("delete from Customers where prenom = @PropertyName", conn);

                //
                // 4. Use the connection
                //

                // 2. define parameters used in command object
                /*OdbcParameter param1 = new OdbcParameter();
                param1.ParameterName = "@TableName";
                param1.Value = tablename;*/

                /*OdbcParameter param2 = new OdbcParameter(); 
                param2.ParameterName = "@ColumnName";
                param2.Value = columnname;*/

                OdbcParameter param3 = new OdbcParameter();
                param3.ParameterName = "@PropertyName";
                param3.Value = propertyname;

                // 3. add new parameter to command object
                //cmd.Parameters.Add(param1);
                //cmd.Parameters.Add(param2);
                cmd.Parameters.Add(param3);


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
        

        public static void DeletePostGre()
        {
            Console.WriteLine("Que voulez-vous supprimer ?");
            /*Console.WriteLine("Entrez le nom de la table");
            string tablename = Console.ReadLine();*/
            /*Console.WriteLine("Entrez le nom de la colonne");
            string columnname = Console.ReadLine();*/
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
                OdbcCommand cmd = new OdbcCommand("delete from customers where prenom = ?", conn);

                // 2. define parameters used in command object
                /*OdbcParameter param1 = new OdbcParameter();
                param1.ParameterName = "@TableName";
                param1.Value = tablename;*/

                /*OdbcParameter param2 = new OdbcParameter(); 
                param2.ParameterName = "@ColumnName";
                param2.Value = columnname;*/

                //OdbcParameter param3 = new OdbcParameter();
                /*param3.ParameterName = "@PropertyName";
                param3.Value = propertyname;*/
                cmd.Parameters.Add("@PropertyName", OdbcType.NVarChar).Value = propertyname;

                // 3. add new parameter to command object
                //cmd.Parameters.Add(param1);
                //cmd.Parameters.Add(param2);
                //cmd.Parameters.Add(param3);


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

    }
}

