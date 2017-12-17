namespace Project_Orn
{
    public class ConnectionMySql
    {
        public ConnectionMySql(string driver, string server, string database, string user, string password)
        {
            Driver = driver;
            Server = server;
            DataBase = database;
            User = user;
            Password = password;
        }

        public string Driver { get; set; }
        public string Server { get; set; }
        public string DataBase { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}