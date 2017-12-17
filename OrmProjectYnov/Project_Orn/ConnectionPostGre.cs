namespace Project_Orn
{
    public class ConnectionPostGre
    {
        public ConnectionPostGre(string driver, string server, string port, string database, string user,
            string password)
        {
            Driver = driver;
            Server = server;
            Port = port;
            DataBase = database;
            User = user;
            Password = password;
        }

        public string Driver { get; set; }
        public string Server { get; set; }
        public string Port { get; set; }
        public string DataBase { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}