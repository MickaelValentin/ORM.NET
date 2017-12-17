using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Reflection;

namespace Project_Orn
{
    /// <summary>
    ///  CRUD + CREATE AND DROP TABLE POSTGRE
    /// </summary>
    public static class PostGreMapping
    {
        #region OldCode

        /*
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
                "Driver={PostgreSQL Unicode ;Server=localhost;Port=5432;Database=orm;UID=mickaël;PWD=170514");


            try
            {
                // 2. Open the connection
                conn.Open();

                // 3. Pass the connection to a command object
                OdbcCommand cmd = new OdbcCommand("create table ? ( ? ? )", conn);

                cmd.Parameters.Add("@TableName", OdbcType.NVarChar).Value = tablename;
                cmd.Parameters.Add("@ColumnName", OdbcType.NVarChar).Value = columnname;
                cmd.Parameters.Add("@ColumnType", OdbcType.NVarChar).Value = columntype;


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

        public static void DropTable()
        {
            Console.WriteLine("Entrez le nom de la table à supprimer");
            string tablename = Console.ReadLine();


            // 1. Instantiate the connection
            OdbcConnection conn = new OdbcConnection(
                "Driver={PostgreSQL ODBC Driver(UNICODE)};Server=localhost;Port=5432;Database=orm;UID=mickaël;PWD=170514");


            try
            {
                // 2. Open the connection
                conn.Open();

                // 3. Pass the connection to a command object
                OdbcCommand cmd = new OdbcCommand("DROP TABLE IF EXISTS ?", conn);

                cmd.Parameters.Add("@TableName", OdbcType.NVarChar).Value = tablename;



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
        */

        #endregion

        #region Insert

        /// <summary>
        /// Insertion d'élément(s) dans une table en base
        /// </summary>
        /// <param name="connection">Connexion à la base</param>
        /// <param name="obj">Objet générique</param>
        /// <returns>Retourne true si la requête a été exécutée en base, une exception si il y a une erreur.</returns>
        /// <exception cref="Exception">Exception déclenchée quand la connexion échoue; l'objet existe déjà en base; quand les types des propriétés ne sont pas respectés; une limite de charactères dépassée. </exception>
        public static bool InsertNextGen<T>(ConnectionPostGre connection, T obj)
        {
            MappingObject objectMapping = new MappingObject();
            objectMapping = MappingOperations.GetTypeOfProPostGre(obj);
            string reqInsertElement = $" INSERT INTO {objectMapping.ObjectName} VALUES(DEFAULT,";
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
                using (OdbcConnection conn = GetConnection(connection.Driver, connection.Server, connection.Port,
                    connection.DataBase, connection.User, connection.Password))

                {
                    conn.Open();
                    using (OdbcCommand queryToInsert = new OdbcCommand(reqInsertElement, conn))
                    {
                        for (int i = 0; i < objectMapping.PropertiesAttributes.Count(); i++)
                        {
                            PropertyAttributes infoFormapping = objectMapping.PropertiesAttributes[i];
                            queryToInsert.Parameters.AddWithValue($"{infoFormapping.NameInfo}",
                                infoFormapping.ValueInfo);
                        }
                        queryToInsert.Prepare();
                        queryToInsert.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion

        #region CreateTable

        /// <summary>
        /// Création de table en base
        /// </summary>
        /// <param name="connection">Connexion à la base</param>
        /// <param name="obj">Objet générique</param>
        /// <returns>Retourne true si la requête a été exécutée en base, une exception si il y a une erreur.</returns>
        /// <exception cref="Exception">Exception déclenchée quand la connexion échoue; l'objet existe déjà en base; quand les types des propriétés ne sont pas respectés; une limite de charactères dépassée. </exception>
        public static bool CreateTableNextGen<T>(ConnectionPostGre connection, T obj)
        {
            MappingObject objectMapping = new MappingObject();
            objectMapping = MappingOperations.GetTypeOfProPostGre(obj);


            string reqCreateTable = $"CREATE TABLE IF NOT EXISTS {objectMapping.ObjectName}(ID SERIAL PRIMARY KEY,";
            for (int i = 0; i < objectMapping.PropertiesAttributes.Count(); i++)
            {
                reqCreateTable +=
                    $"{objectMapping.PropertiesAttributes[i].NameInfo} {objectMapping.PropertiesAttributes[i].TypeInfo}";
                if (i != objectMapping.PropertiesAttributes.Count() - 1)
                {
                    reqCreateTable += ",";
                }
            }

            reqCreateTable += ")";
            try
            {
                using (OdbcConnection conn = GetConnection(connection.Driver, connection.Server, connection.Port,
                    connection.DataBase, connection.User, connection.Password))

                {
                    conn.Open();
                    using (OdbcCommand queryToCreateTable = new OdbcCommand(reqCreateTable, conn))
                    {
                        queryToCreateTable.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion

        #region Select

        /// <summary>
        /// Lis et affiche les données d'une table en base
        /// </summary>
        /// <param name="connection">Connexion à la base.</param>
        /// <param name="column">Nom de(s) colonne(s) de la table</param>
        /// <param name="value">Valeur(s) de la table</param>
        /// <param name="table">Objet générique table</param>
        /// <returns>Retourne une liste si la requête a été exécutée en base, une exception si il y a une erreur.</returns>
        /// <exception cref="Exception">Exception déclenchée quand la connexion échoue; l'objet existe déjà en base; quand les types des propriétés ne sont pas respectés; une limite de charactères dépassée. </exception>
        public static List<T> SelectTableNextGen<T>(ConnectionPostGre connection, string column, string value, T table)
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
                using (OdbcConnection conn = GetConnection(connection.Driver, connection.Server, connection.Port,
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
                throw new Exception(e.Message);
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// Supprime un element d'une table en base
        /// </summary>
        /// <param name="connection">Connexion à la base.</param>
        /// <param name="column">Nom de(s) colonne(s) de la table</param>
        /// <param name="value">Valeur(s) de la table</param>
        /// <param name="table">Objet générique table</param>
        /// <returns>Retourne true si la requête a été exécutée en base, une exception si il y a une erreur.</returns>
        /// <exception cref="Exception">Exception déclenchée quand la connexion échoue; l'objet existe déjà en base; quand les types des propriétés ne sont pas respectés; une limite de charactères dépassée. </exception>
        public static bool DeleteElemetFromTableNextGen<T>(ConnectionPostGre connection, string column, string value,
            T table)
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
                using (OdbcConnection conn = GetConnection(connection.Driver, connection.Server, connection.Port,
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
                throw new Exception(e.Message);
            }
        }

        #endregion

        #region DropTable

        /// <summary>
        /// Supprime une table en base
        /// </summary>
        /// <param name="connection">Connexion à la base.</param>
        /// <param name="obj">Objet générique.</param>
        /// <returns>Retourne true si la requête a été exécutée en base, une exception si il y a une erreur.</returns>
        /// <exception cref="Exception">Exception déclenchée quand la connexion échoue; l'objet existe déjà en base; quand les types des propriétés ne sont pas respectés; une limite de charactères dépassée. </exception>
        public static bool DropTableNextGen<T>(ConnectionPostGre connection, T obj)
        {
            MappingObject objectMapping = new MappingObject();
            objectMapping = MappingOperations.GetTypeOfProPostGre(obj);
            string reqDropTable = $"DROP TABLE IF EXISTS {objectMapping.ObjectName}";
            try
            {
                using (OdbcConnection conn = GetConnection(connection.Driver, connection.Server, connection.Port,
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// Modifie le ou les élément(s) d'une table en base
        /// </summary>
        /// <param name="connection">Connexion à la base.</param>
        /// <param name="id">Id des propriétés à modifier </param>
        /// <param name="table">Objet générique table</param>
        /// <returns>Retourne true si la requête a été exécutée en base, une exception si il y a une erreur.</returns>
        /// <exception cref="Exception">Exception déclenchée quand la connexion échoue; l'objet existe déjà en base; quand les types des propriétés ne sont pas respectés; une limite de charactères dépassée. </exception>
        public static bool UpdateElementNextGen<T>(ConnectionPostGre connection, int id, T table)
        {
            if (table.GetType().Name == null)
            {
                Console.WriteLine("obj not found");
                return false;
            }
            MappingObject objectMapping = new MappingObject();
            objectMapping = MappingOperations.GetTypeOfProPostGre(table);
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
                using (OdbcConnection conn = GetConnection(connection.Driver, connection.Server, connection.Port,
                    connection.DataBase, connection.User, connection.Password))

                {
                    conn.Open();
                    using (OdbcCommand queryUpdate = new OdbcCommand(reqUpdate, conn))
                    {
                        for (int i = 0; i < objectMapping.PropertiesAttributes.Count(); i++)
                        {
                            PropertyAttributes infoFormapping = objectMapping.PropertiesAttributes[i];
                            queryUpdate.Parameters.AddWithValue($"{infoFormapping.NameInfo}", infoFormapping.ValueInfo);
                        }
                        queryUpdate.Parameters.AddWithValue($"id", id);
                        queryUpdate.Prepare();
                        queryUpdate.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion

        #region GetConnection

        /// <summary>
        /// Connexion à la base
        /// </summary>
        /// <param name="driver">Nom du driver de connexion à la base.</param>
        /// <param name="server">Adresse IP de la base.</param>
        /// <param name="port">Port de connexion à la base.</param>
        /// <param name="database">Nom de la base.</param>
        /// <param name="user">Identifiant utilisateur de la base.</param>
        /// <param name="password">Mot de passe utilisateur de la base.</param>
        /// <returns>Retourne la chaîne de connexion permettant de se connecter à la base</returns>
        private static OdbcConnection GetConnection(string driver, string server, string port,
            string database, string user, string password)
        {
            return new OdbcConnection(
                $"DRIVER={{{driver}}};" +
                $"SERVER={server};" +
                $"PORT={port};" +
                $"DATABASE={database};" +
                $"Uid={user};" +
                $"Pwd={password}");
        }
        #endregion
    }
}