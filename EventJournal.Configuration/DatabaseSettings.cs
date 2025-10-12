namespace EventJournal.Configuration {
    public enum DatabaseProvider {
        Sqlite,
        // TODO:
        // MySql,
        // SqlServer,
        // PostgreSQL
    }
    public class DatabaseSettings : IServiceSettings {
        public static string ConfigurationSectionName => "Database";
        public string ConnectionString { get; set; } = null!;
        public DatabaseProvider DefaultProvider { get; set; } = DatabaseProvider.Sqlite;
        public bool UseDefaultConnectionString => string.IsNullOrWhiteSpace(ConnectionString) || ConnectionString.Equals("default", StringComparison.CurrentCultureIgnoreCase);

    }
}
