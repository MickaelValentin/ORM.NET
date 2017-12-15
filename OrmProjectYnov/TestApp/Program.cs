using TestApp.Metier;
using System;
using System.Collections.Generic;
using Project_Orn;
using System.Data.SqlClient;
using System.Data.Odbc;

namespace TestApp
{
    class Program
    {

        //private static DateTime debut;
        //private static DateTime fin;

        static void Main(string[] args)
        {
            Voiture ka = new Voiture
            {
                Brand = "Ford",
                Model = "Ka",
                CreateDate = DateTime.Today.AddDays(-1),
                Distance = 100454356578,
                Power = 4,
                Sizecar = 5.25,
                Isitok = true
            };

            Voiture mustang = new Voiture
            {
                Brand = "Ford",
                Model = "Mustang",
                CreateDate = DateTime.Today.AddDays(5),
                Distance = 100,
                Power = 360,
                Sizecar = 5.25,
                Isitok = true
            };

            Voiture golf = new Voiture
            {
                Brand = "Volkswagen",
                Model = "Golf 7",
                CreateDate = DateTime.Today,
                Distance = 2500,
                Power = 120,
                Sizecar = 4.25,
                Isitok = true
            };

            Voiture aventador = new Voiture
            {
                Brand = "Lamborghini",
                Model = "Aventador",
                CreateDate = DateTime.Today,
                Distance = 2500,
                Power = 120,
                Sizecar = 4.25,
                Isitok = true
            };


            //Test of MySQL 
            ConnectionMySql MysqlConnection = new ConnectionMySql("MySQL ODBC 5.3 ANSI Driver", "localhost",
                                                                        "test", "root", "root");
         
             MySqlMapping.DropTableNextGen(MysqlConnection, new Voiture());
             MySqlMapping.CreateTableNextGen(MysqlConnection,new Voiture());
             MySqlMapping.InsertNextGen(MysqlConnection,ka);
             MySqlMapping.InsertNextGen(MysqlConnection,mustang);
             MySqlMapping.InsertNextGen(MysqlConnection,golf);
             MySqlMapping.InsertNextGen(MysqlConnection,aventador);
             List<Voiture> GarageFord = MySqlMapping.SelectTableNextGen(MysqlConnection,"Brand", "Ford", new Voiture());
             ka.Power = 250;
             MySqlMapping.UpdateElementNextGen(MysqlConnection,1, ka);
             MySqlMapping.DeleteElemetFromTableNextGen(MysqlConnection,"Brand", "Lamborghini", new Voiture());




            //Test of PostGre
            //PostGreMapping.DropTableNextGen(new Voiture());
            //PostGreMapping.CreateTableNextGen(new Voiture());
            //PostGreMapping.InsertNextGen(ka);
            //PostGreMapping.InsertNextGen(mustang);
            //PostGreMapping.InsertNextGen(golf);
            //PostGreMapping.InsertNextGen(aventador);
            //List<Voiture> GarageFord2 = PostGreMapping.SelectTableNextGen("Brand", "Ford", new Voiture());

            //PostGreMapping.DeleteElemetFromTableNextGen("Brand", "Lamborghini", new Voiture());
            //mustang.Power = 2500;
            //PostGreMapping.UpdateElementNextGen(2, mustang);




            //Test of SQL Server 
           // SqlConnection connSql = SQLServerMapping.GetConnection("(local)", "testorm", "dinesh", "root1234");            // SQLServerMapping.DropTableNextGen(new Voiture());
           // SQLServerMapping.CreateTableNextGen(connSql,new Voiture());
            // SQLServerMapping.InsertNextGen(ka);
            // SQLServerMapping.InsertNextGen(mustang);
            // SQLServerMapping.InsertNextGen(golf);
            // SQLServerMapping.InsertNextGen(aventador);

            // List<Voiture> GarageFord = SQLServerMapping.SelectTableNextGen("Brand", "Ford", new Voiture());
            //// SQLServerMapping.DeleteElemetFromTableNextGen("Brand", "Ford", new Voiture());
            // ka.Power = 250;
            // SQLServerMapping.UpdateElementNextGen(4, ka);
            // SQLServerMapping.DeleteElemetFromTableNextGen("Brand", "Lamborghini", new Voiture());

            Console.WriteLine("End");


        }


    }
}
