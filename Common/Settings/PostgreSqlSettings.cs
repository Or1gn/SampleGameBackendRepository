namespace Common.Settings
{
    //Host=localhost;Port=5432;Database=rpg;Username=postgres;Password=qRV5jSQXsn2&$6
    public class PostgreSqlSettings
    {
        public string Host { get; init; }
        public int Port { get; init; }
        public string DatabaseName { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
        public string ConnectionString => $"Host={Host};Port={Port};Database={DatabaseName};Username={Username};Password={Password}";
    }
}
