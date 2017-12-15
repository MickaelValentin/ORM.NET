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
                Distance = 25324,
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


            ////Test of MySQL 
            ConnectionMySql connectionMySql = new ConnectionMySql("MySQL ODBC 5.3 ANSI Driver", "localhost",
                                                                         "test", "root", "root");
            MySqlMapping.DropTableNextGen(connectionMySql, new Voiture());
            MySqlMapping.CreateTableNextGen(connectionMySql, new Voiture());
            MySqlMapping.InsertNextGen(connectionMySql, ka);
            MySqlMapping.InsertNextGen(connectionMySql, mustang);
            MySqlMapping.InsertNextGen(connectionMySql, golf);
            MySqlMapping.InsertNextGen(connectionMySql, aventador);
            List<Voiture> GarageMySql = MySqlMapping.SelectTableNextGen(connectionMySql, "Brand", "Ford", new Voiture());
            ka.Power = 250;
            MySqlMapping.UpdateElementNextGen(connectionMySql, 1, ka);
            MySqlMapping.DeleteElemetFromTableNextGen(connectionMySql, "Brand", "Lamborghini", new Voiture());




            //Test of PostGre
            ConnectionPostGre connectionPostGre = new ConnectionPostGre("PostgreSQL Unicode", "localhost", "5432",
                                                                       "testorm", "postgres", "root");
            PostGreMapping.DropTableNextGen(connectionPostGre, new Voiture());
            PostGreMapping.CreateTableNextGen(connectionPostGre, new Voiture());
            PostGreMapping.InsertNextGen(connectionPostGre, ka);
            PostGreMapping.InsertNextGen(connectionPostGre, mustang);
            PostGreMapping.InsertNextGen(connectionPostGre, golf);
            PostGreMapping.InsertNextGen(connectionPostGre, aventador);
            List<Voiture> GaragePostGre = PostGreMapping.SelectTableNextGen(connectionPostGre, "Brand", "Ford", new Voiture());

            PostGreMapping.DeleteElemetFromTableNextGen(connectionPostGre, "Brand", "Lamborghini", new Voiture());
            mustang.Power = 2501;
            PostGreMapping.UpdateElementNextGen(connectionPostGre, 2, mustang);




            //Test of SQL Server 
            ConnectionSqlServer connectionSqlServer = new ConnectionSqlServer("(local)", "testorm", "dinesh", "root1234");
            SQLServerMapping.DropTableNextGen(connectionSqlServer, new Voiture());
            SQLServerMapping.CreateTableNextGen(connectionSqlServer, new Voiture());
            SQLServerMapping.InsertNextGen(connectionSqlServer, ka);
            SQLServerMapping.InsertNextGen(connectionSqlServer, mustang);
            SQLServerMapping.InsertNextGen(connectionSqlServer, golf);
            SQLServerMapping.InsertNextGen(connectionSqlServer, aventador);

             List<Voiture> GarageSqlServer = SQLServerMapping.SelectTableNextGen(connectionSqlServer,"Brand", "Ford", new Voiture());
             SQLServerMapping.DeleteElemetFromTableNextGen(connectionSqlServer, "Model", "Ka", new Voiture());
             aventador.Power = 783;
             SQLServerMapping.UpdateElementNextGen(connectionSqlServer, 4, aventador);
            // SQLServerMapping.DeleteElemetFromTableNextGen("Brand", "Lamborghini", new Voiture());

            Console.WriteLine("End");


        }


    }
}
