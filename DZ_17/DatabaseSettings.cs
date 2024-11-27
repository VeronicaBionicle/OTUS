namespace DZ_17
{
    public class DatabaseSettings
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string UserId { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }

        public string CreateDatabaseConnectionString()
        {
            return $"Server={Server}; Port={Port}; Database={Database}; User ID={UserId}; Password={Password};";
        }
    }
}
