namespace Project_Orn
{
    public class ConnectionSqlServer
    {
        public ConnectionSqlServer(string server, string database, string user, string password)
        {
            Server = server;
            DataBase = database;
            User = user;
            Password = password;
        }

        public string Server { get; set; }
        public string DataBase { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}