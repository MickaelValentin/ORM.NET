using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrnProject;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data;

namespace OrnProject
{
    class CSD_ODB
    {
        public static void createTable()
        {
            OdbcConnection connexion = new OdbcConnection(


            "DRIVER={MySQL ODBC 5.3 ANSI Driver};" +
                   "SERVER=localhost;" +
                   "DATABASE=orm;" +
                   "USER=root;");

            Console.WriteLine("Créer ->");
            Console.WriteLine("Table : ?");
            string nomTable = Console.ReadLine();

            Console.WriteLine($"Nombre de colonnes table {nomTable} : ?");
            string nombreColonne = Console.ReadLine();
            int nombre = Int32.Parse(nombreColonne);

            string nomCol;
            string nomAllCol = "";
            string type;
            string taille;

            for (int i = 0; i < nombre; i++)
            {
                Console.WriteLine($"Colonne {i + 1} : ");
                nomCol = Console.ReadLine();

                Console.WriteLine($"Type de la colonne {nomCol} : ");
                type = Console.ReadLine();

                Console.WriteLine($"Taille/Valeur : ");
                taille = Console.ReadLine();

                type.ToLower();

                if (i == 0)
                {
                    nomAllCol = $"{nomCol} {type}({taille})";
                }
                else
                {
                    nomAllCol = $"{nomAllCol},{nomCol} {type}({taille})";   //On concatène les chaînes
                }

            }

            try
            {
                connexion.Open();   //On ouvre la connexion

                OdbcCommand cmd = new OdbcCommand($"CREATE TABLE {nomTable} ({nomAllCol})", connexion);
                cmd.ExecuteNonQuery();

                Console.WriteLine("La table à bien été créer");
            }

            finally
            {


                if (connexion != null)
                {
                    connexion.Close();  //On ferme la connexion
                }
            }
        }

        public static void selectTable()
        {
            OdbcConnection connexion = new OdbcConnection(


           "DRIVER={MySQL ODBC 5.3 ANSI Driver};" +
                  "SERVER=localhost;" +
                  "DATABASE=orm;" +
                  "USER=root;");

            Console.WriteLine("Rechercher ->");
            try
            {
                connexion.Open();   //On ouvre la connexion

                Console.WriteLine("Table : ?");
                string nomTable = Console.ReadLine();

                Console.WriteLine("Colonne : ? (* pour tout lister)");
                string nomColonne = Console.ReadLine();

                OdbcCommand cmd = new OdbcCommand($"SELECT {nomColonne} FROM {nomTable}", connexion);
                OdbcDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine(reader[1]);

                }
            }

            finally
            {


                if (connexion != null)
                {
                    connexion.Close();  //On ferme la connexion
                }
            }
        }

        public static void dropTable()
        {
            OdbcConnection connexion = new OdbcConnection(


           "DRIVER={MySQL ODBC 5.3 ANSI Driver};" +
                  "SERVER=localhost;" +
                  "DATABASE=orm;" +
                  "USER=root;");

            Console.WriteLine("Supprimer ->");
            try
            {
                connexion.Open();    //On ouvre la connexion

                Console.WriteLine("1 - Supprimer une table");
                Console.WriteLine("2 - Supprimer une colonne");

                string switchString = Console.ReadLine();
                int caseSwitch = Int32.Parse(switchString);

                Console.WriteLine("Table :  ?");
                string nomTable = Console.ReadLine();

                switch (caseSwitch)
                {
                    case 1:
                        OdbcCommand cmdTable = new OdbcCommand($"DROP TABLE IF EXISTS {nomTable}", connexion);   //Objet OdbcCommand avec requête pour drop la table en paramètre
                        cmdTable.ExecuteNonQuery();
                        break;
                    case 2:
                        Console.WriteLine("Colonne : ?");
                        string nomColonne = Console.ReadLine();

                        OdbcCommand cmdCol = new OdbcCommand($"ALTER TABLE {nomTable} DROP COLUMN {nomColonne}", connexion); ////Objet OdbcCommand avec requête pour drop une colonne de la table en paramètre
                        cmdCol.ExecuteNonQuery();
                        break;
                    default:
                        Console.WriteLine("Une erreur est survenu");
                        break;
                }

                Console.WriteLine($"La table {nomTable} à bien été suprimer");
            }

            finally
            {

                if (connexion != null)
                {
                    connexion.Close();   //On ferme la connexion
                }
            }
        }

        public static void delete()
        {
            OdbcConnection connexion = new OdbcConnection(


           "DRIVER={MySQL ODBC 5.3 ANSI Driver};" +
                  "SERVER=localhost;" +
                  "DATABASE=orm;" +
                  "USER=root;");

            Console.WriteLine("Supprimer ->");
            try
            {
                connexion.Open();    //On ouvre la connexion

                string nomCol;
                string valeur;
                string colonneValeur = "";

                Console.WriteLine("Table : ?");
                string nomTable = Console.ReadLine();

                OdbcCommand testtt = new OdbcCommand($"SELECT * FROM {nomTable}", connexion);    //On crée un objet OdbcCommand avec une requête en paramètre pour récupérer les informations
                OdbcDataReader datareader = testtt.ExecuteReader();

                DataTable test = new DataTable();   //On crée un objet DataTable

                test.Load(datareader);  //On charge le résultat de la requête

                int nombreCol = test.Columns.Count; //On récupère le nombre de colonnes

                for (int i = 0; i < nombreCol; i++)
                {
                    Console.WriteLine($"Colonne {i + 1} : ");
                    nomCol = Console.ReadLine();

                    Console.WriteLine($"Valeur colonne {i+1} : ?");
                    valeur = Console.ReadLine();

                    if (i == 0)
                    {
                        colonneValeur = $"{nomCol} = '{valeur}'";
                    }
                    else
                    {
                        colonneValeur = $"{colonneValeur} AND {nomCol} = '{valeur}'";    //On concatène les chaînes
                    }
                }

                OdbcCommand cmd = new OdbcCommand($"DELETE FROM {nomTable} WHERE {colonneValeur} ", connexion);  //On crée un objet OdbcCommand avec la nouvelle requête
                cmd.ExecuteNonQuery();

            }

            finally
            {


                if (connexion != null)
                {
                    connexion.Close();  //On ferme la connexion
                }
            }
        }

        public static void insert()
        {
            OdbcConnection connexion = new OdbcConnection(


           "DRIVER={MySQL ODBC 5.3 ANSI Driver};" +
                  "SERVER=localhost;" +
                  "DATABASE=orm;" +
                  "USER=root;");

            Console.WriteLine("Insérer ->");

            string nomTable;
            string nomColonne;
            string valeur;
            string allColonne = "";
            string allValeur = "";

            try
            {
                connexion.Open();

                Console.WriteLine("Table : ?");
                nomTable = Console.ReadLine();

                OdbcCommand testtt = new OdbcCommand($"SELECT * FROM {nomTable}", connexion);    //On crée un objet OdbcCommand avec une requête en paramètre pour récupérer les informations
                OdbcDataReader datareader = testtt.ExecuteReader();

                DataTable dataTable = new DataTable();   //On crée un objet DataTable

                dataTable.Load(datareader);  //On charge le résultat de la requête

                int nombreCol = dataTable.Columns.Count; //On récupère le nombre de colonnes

                for (int i = 0; i < nombreCol; i++)
                {
                    Console.WriteLine($"Nom colonne {i + 1} : ?");
                    nomColonne = Console.ReadLine();

                    Console.WriteLine($"Valeur colonne {i + 1} : ?");
                    valeur = Console.ReadLine();

                    if (i == 0)
                    {
                        allColonne = nomColonne;
                        allValeur = $"'{valeur}'";
                    }
                    else
                    {
                        allColonne = $"{allColonne}, {nomColonne}";     //On concatène les chaînes
                        allValeur = $"{allValeur}, '{valeur}'";
                    }


                }

                OdbcCommand cmd = new OdbcCommand($"INSERT INTO {nomTable} ({allColonne}) VALUES ({allValeur})", connexion);    //On crée un objet OdbcCommand avec la requête en paramètre
                cmd.ExecuteNonQuery();


            }

            finally
            {


                if (connexion != null)
                {
                    connexion.Close();  //On ferme la connexion
                }
            }
        }

        public static void update()
        {
            OdbcConnection connexion = new OdbcConnection(


           "DRIVER={MySQL ODBC 5.3 ANSI Driver};" +
                  "SERVER=localhost;" +
                  "DATABASE=orm;" +
                  "USER=root;");

            Console.WriteLine("Update ->");

            string nomColonne;
            string valeur;
            string colonneValeur = "";

            try
            {
                connexion.Open();

                Console.WriteLine("Table : ?");
                string nomTable = Console.ReadLine();

                Console.WriteLine("Nombre de valeur à update : ?");
                string nombreValeur = Console.ReadLine();
                int nombre = Int32.Parse(nombreValeur);

                for (int i = 0; i < nombre; i++)
                {
                    Console.WriteLine($"Colonne à modifier : ?");
                    nomColonne = Console.ReadLine();

                    Console.WriteLine($"Nouvelle valeur : ?");
                    valeur = Console.ReadLine();

                    if (i == 0)
                    {
                        colonneValeur = $"{nomColonne} = '{valeur}'";
                    }
                    else
                    {
                        colonneValeur = $"{colonneValeur}, {nomColonne} = '{valeur}'";    //On concatène les chaînes
                    }
                }

                Console.WriteLine("Colonne condition : ?");
                string colCondition = Console.ReadLine();

                Console.WriteLine("Condition (ID): ?");
                string condition = Console.ReadLine();

                OdbcCommand cmd = new OdbcCommand($"UPDATE {nomTable} SET {colonneValeur} WHERE {colCondition} = {condition}", connexion);    //On crée un objet OdbcCommand avec la requête en paramètre
                cmd.ExecuteNonQuery();


            }

            finally
            {


                if (connexion != null)
                {
                    connexion.Close();  //On ferme la connexion
                }
            }
        }

    }
}


