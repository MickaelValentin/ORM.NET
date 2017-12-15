using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;

using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Project_Orn
{
    public static class MySqlMapping
    {
        #region Old Code
        /*
        public static void createSqlTable()
        {
            OdbcConnection conn = new OdbcConnection(
            "DRIVER={MySQL ODBC 5.3 ANSI Driver};" +
                   "SERVER=localhost;" +
                   "DATABASE=orm;" +
                   "USER=root;");

            Console.WriteLine("Créer ->");
            Console.WriteLine("Enter your table name");
            string nomTable = Console.ReadLine();
            Console.WriteLine($"Entrer le nombre de colonnes de la table {nomTable}");
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
                    nomAllCol = $"{nomAllCol},{nomCol} {type}({taille})";
                }

            }

            try
            {
                // 2. Open the connection
                conn.Open();

                //OdbcCommand cmd = new OdbcCommand($"CREATE TABLE {nomTable}()", conn);
                OdbcCommand cmd = new OdbcCommand($"CREATE TABLE {nomTable} ({nomAllCol})", conn);
                cmd.ExecuteNonQuery();

                
e.WriteLine("La table à bien été créer");
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

            Console.WriteLine("Rechercher ->");
            try
            {
                // 2. Open the connection
                conn.Open();

                Console.WriteLine("Table : ?");
                string nomTable = Console.ReadLine();

                Console.WriteLine("Colonne : ? (* pour tout lister)");
                string nomColonne = Console.ReadLine();

                //OdbcCommand cmd = new OdbcCommand($"CREATE TABLE {nomTable}()", conn);
                OdbcCommand cmd = new OdbcCommand($"SELECT {nomColonne} FROM {nomTable}", conn);
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

            Console.WriteLine("Supprimer ->");
            try
            {
                // 2. Open the connection
                conn.Open();

                Console.WriteLine("1 - Supprimer une table");
                Console.WriteLine("2 - Supprimer une colonne");

                string switchString = Console.ReadLine();
                int caseSwitch = Int32.Parse(switchString);

                Console.WriteLine("Table :  ?");
                string nomTable = Console.ReadLine();

                switch (caseSwitch)
                {
                    case 1:
                        OdbcCommand cmdTable = new OdbcCommand($"DROP TABLE IF EXISTS {nomTable}", conn);
                        cmdTable.ExecuteNonQuery();
                        break;
                    case 2:
                        Console.WriteLine("Colonne : ?");
                        string nomColonne = Console.ReadLine();

                        OdbcCommand cmdCol = new OdbcCommand($"ALTER TABLE {nomTable} DROP COLUMN {nomColonne}", conn);
                        cmdCol.ExecuteNonQuery();
                        break;
                    default:
                        Console.WriteLine("Une erreur est survenu");
                        break;
                }




                //cmd.ExecuteNonQuery();

                Console.WriteLine($"La table {nomTable} à bien été suprimer");
                Console.WriteLine("Appuyer sur une touche pour continuer");
                Console.ReadLine();
            }

            finally
            {


                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public static void insert()
        {
            OdbcConnection conn = new OdbcConnection(


           "DRIVER={MySQL ODBC 5.3 ANSI Driver};" +
                  "SERVER=localhost;" +
                  "DATABASE=orm;" +
                  "USER=root;");

            Console.WriteLine("Insérer ->");

            string nomTable;
            string nomColonne;
            string valeur;
            string nombreColonne;
            int nombre;
            string allColonne = "";
            string allValeur = "";

            try
            {
                conn.Open();

                Console.WriteLine("Table : ?");
                nomTable = Console.ReadLine();

                Console.WriteLine("Nombre colonne : ?");
                nombreColonne = Console.ReadLine();
                nombre = Int32.Parse(nombreColonne);

                for (int i = 0; i < nombre; i++)
                {
                    Console.WriteLine($"Nom colonne {i + 1} : ?");
                    nomColonne = Console.ReadLine();

                    Console.WriteLine($"Valeur colonne {i + 1} : ?");
                    valeur = Console.ReadLine();

                    if (i == 0)
                    {
                        allColonne = nomColonne;
                        allValeur = valeur;
                    }
                    else
                    {
                        allColonne = $"{allColonne}, {nomColonne}";
                        allValeur = $"{allValeur}, {valeur}";
                    }


                }

                OdbcCommand cmd = new OdbcCommand($"INSERT INTO {nomTable} ({allColonne}) VALUES ({allValeur})", conn);
                cmd.ExecuteReader();


            }

            finally
            {


                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        */
        #endregion

        #region Insert

        /// <summary>
        /// Méthode Insert
        /// </summary>
        /// <typeparam name="T">Paramètre générique</typeparam>
        /// <param name="obj">Objet voiture</param>
        /// <remarks>
        /// obj créer et envoyer en paramètre à la fonction dans le fichier Program.cs
        /// <see cref="TestApp.Program.Main"></see>
        /// </remarks>
        /// <c>reqInsertElement</c> Contient la syntaxe de la requête MySql
        /// <c>objectMapping</c>Contient les information de l'objet voiture
        /// <param name="conn">Contient les informations de connexion -> (nom de la base, identifiant, mot de passe..)</param>
        /// <remarks>
        /// <c>infoFormapping"</c>
        /// Récupère les attributs de la table
        /// Puis ajoute les valeurs pour chaque attribut de la table
        /// <code>
        /// PropertyAttributes infoFormapping = objectMapping.PropertiesAttributes[i];
        /// qureyToInsert.Parameters.AddWithValue($"{infoFormapping.NameInfo}", infoFormapping.ValueInfo);
        /// </code>
        /// </remarks>
        /// <returns>True si la requête s'est executer correctement, false si une exception est détecté</returns>

        public static bool InsertNextGen<T>(ConnectionMySql connection, T obj)
        {
            MappingObject objectMapping = new MappingObject();
            objectMapping = MappingOperations.GetTypeOfProMySQL(obj);
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
                using (OdbcConnection conn = GetConnection(connection.Driver, connection.Server,
                    connection.DataBase, connection.User, connection.Password))

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
        #endregion

        #region Create

        /// <summary>
        /// Méthode Create
        /// </summary>
        /// <typeparam name="T">Paramètre générique</typeparam>
        /// <param name="obj">Objet voiture</param>
        /// <remarks>
        /// obj créer et envoyer en paramètre à la fonction dans le fichier Program.cs
        /// <see cref="TestApp.Program.Main"></see>
        /// </remarks>
        /// <remarks>
        /// <c>reqCreateTable</c> Contient la syntaxe de la requête MySql
        /// <c>objectMapping</c>Contient les information de l'objet voiture
        /// <c>conn</c>Contient les informations de connexion -> (nom de la base, identifiant, mot de passe..)
        /// </remarks>
        /// <remarks>
        /// On récupère grâce à objectMapping les informations de l'objet que l'on ajoute à la requête MySql
        /// <c>reqCreateTable += $"{objectMapping.PropertiesAttributes[i].NameInfo} {objectMapping.PropertiesAttributes[i].TypeInfo},";</c>
        /// Le nom de l'attribut est récupérer ainsi que le type de sa valeur
        /// </remarks>
        /// <returns>True si la requête s'est executer correctement, false si une exception est détecté</returns>

        public static bool CreateTableNextGen<T>(ConnectionMySql connection, T obj)
        {
            MappingObject objectMapping = new MappingObject();
            objectMapping = MappingOperations.GetTypeOfProMySQL(obj);
            string reqCreateTable = $"CREATE TABLE IF NOT EXISTS {objectMapping.ObjectName}(ID int NOT NULL AUTO_INCREMENT,";
            for (int i = 0; i < objectMapping.PropertiesAttributes.Count(); i++)
            {
                reqCreateTable += $"{objectMapping.PropertiesAttributes[i].NameInfo} {objectMapping.PropertiesAttributes[i].TypeInfo},";
            }
            reqCreateTable += "PRIMARY KEY(ID))";
            try
            {
                using (OdbcConnection conn = GetConnection(connection.Driver, connection.Server,
                  connection.DataBase, connection.User, connection.Password))
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
        #endregion

        #region Select

        /// <summary>
        /// Méthode Select
        /// </summary>
        /// <typeparam name="T">Type générique</typeparam>
        /// <param name="column">Nom de la colonne de la nouvelle table</param>
        /// <param name="value">Nom de la valeur de la nouvelle table</param>
        /// <param name="table">Nom de la nouvelle Table</param>
        /// <remarks>
        /// <c>reqSelectElement</c> Contient la syntaxe de la requête MySql
        /// <c>objectMapping</c>Contient les information de l'objet voiture
        /// <c>conn</c>Contient les informations de connexion -> (nom de la base, identifiant, mot de passe..)
        /// </remarks>
        /// <remarks>
        /// <code>queryToSelectElement.Parameters.AddWithValue(column, value);</code>
        /// Ajoute les élément de recherche à la requête MySql
        /// <code>dt.Load(dr);</code>
        /// Charge les éléments suite au SELECT
        /// <code>List<T> list = MappingOperations.MapList(dt, table);</code>
        /// Ajoute les éléments charger à la liste <c>list</c>
        /// </remarks>
        /// <returns>list si la requête est valide, false si une exeption est détecté</returns>

        public static List<T> SelectTableNextGen<T>(ConnectionMySql connection, string column, string value, T table)
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
                using (OdbcConnection conn = GetConnection(connection.Driver, connection.Server,
     connection.DataBase, connection.User, connection.Password))
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
        #endregion

        #region Delete

        /// <summary>
        /// Méthode Delete
        /// </summary>
        /// <typeparam name="T">Type générique</typeparam>
        /// <param name="column">Nom de la colonne</param>
        /// <param name="value">Nom de la valeur</param>
        /// <param name="table">Nom de la Table</param>
        /// <remarks>
        /// <c>reqDelete</c> Contient la syntaxe de la requête MySql
        /// <c>conn</c>Contient les informations de connexion -> (nom de la base, identifiant, mot de passe..)
        /// </remarks>
        /// <remarks>
        /// Création de l'objet <c>OdbcCommand queryToDeleteElement</c> qui prend en paramètre la requête MySql
        /// <code>queryToDeleteElement.Parameters.AddWithValue(column, value);</code>
        /// Ajout de la valeur à supprimer et de la colonne ou chercher
        /// </remarks>
        /// <returns>true si la requête c'est dérouler correctement, false si une exeption est détecté</returns>

        public static bool DeleteElemetFromTableNextGen<T>(ConnectionMySql connection, string column, string value, T table)
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
                using (OdbcConnection conn = GetConnection(connection.Driver, connection.Server,
            connection.DataBase, connection.User, connection.Password))
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
        #endregion

        #region Drop

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Type générique</typeparam>
        /// <param name="obj">Objet voiture</param>
        /// <remarks>
        /// <c>reqDropTable</c> Contient la syntaxe de la requête MySql
        /// <c>conn</c>Contient les informations de connexion -> (nom de la base, identifiant, mot de passe..)
        /// </remarks>
        /// <remarks>
        /// Avec l'objet <c>objectMapping</c> le nom de la table est récupérer puis ajouter dans la string qui contient la requête MySql
        /// </remarks>
        /// <returns>true si la requête c'est dérouler correctement, false si une exeption est détecté</returns>

        public static bool DropTableNextGen<T>(ConnectionMySql connection, T obj)
        {
            MappingObject objectMapping = new MappingObject();
            objectMapping = MappingOperations.GetTypeOfProMySQL(obj);
            string reqDropTable = $"DROP TABLE IF EXISTS {objectMapping.ObjectName}";
            try
            {
                using (OdbcConnection conn = GetConnection(connection.Driver, connection.Server,
                   connection.DataBase, connection.User, connection.Password))
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
        #endregion

        #region Update

        /// <summary>
        /// Méthode Update
        /// </summary>
        /// <typeparam name="T">Type générique</typeparam>
        /// <param name="id">ID de la table (permet de gérer les conditions)</param>
        /// <param name="table">Information de la Table</param>
        /// <remarks>
        /// <c>reqUpdate</c> Contient la syntaxe de la requête MySql
        /// <c>conn</c>Contient les informations de connexion -> (nom de la base, identifiant, mot de passe..)
        /// </remarks>
        /// <remarks>
        /// Avec l'objet <c>objectMapping</c> les informations relative à la table son récupérer (nom de la table, nombre de colonnes, nom colonne..)
        /// On stock les valeurs à ajouter dans l'objet <c>infoFormapping</c> avant de les ajouter à la requête MySql pour l'execution
        /// <code>
        /// PropertyAttributes infoFormapping = objectMapping.PropertiesAttributes[i];
        /// qureyUpdate.Parameters.AddWithValue($"{infoFormapping.NameInfo}", infoFormapping.ValueInfo);
        /// </code>
        /// On ajoute aussi l'ID pour ajouter la valeur à l'ID correspondant
        /// <code>qureyUpdate.Parameters.AddWithValue($"id", id);</code>
        /// </remarks>
        /// <returns>true si la requête c'est dérouler correctement, false si une exeption est détecter</returns>

        public static bool UpdateElementNextGen<T>(ConnectionMySql connection, int id, T table)
        {
            if (table.GetType().Name == null)
            {
                Console.WriteLine("obj not found");
                return false;

            }
            MappingObject objectMapping = new MappingObject();
            objectMapping = MappingOperations.GetTypeOfProMySQL(table);
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
                using (OdbcConnection conn = GetConnection(connection.Driver, connection.Server,
                 connection.DataBase, connection.User, connection.Password))
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
        #endregion

        #region Get Connection
        public static OdbcConnection GetConnection(string driver, string server,
            string database, string user, string password)
        {
            return new OdbcConnection(
                $"DRIVER={{{driver}}};" +
                $"SERVER={server};" +
                $"DATABASE={database};" +
                $"USER={user};" +
                $"PASSWORD={password}");
        }
        #endregion

    }
}

