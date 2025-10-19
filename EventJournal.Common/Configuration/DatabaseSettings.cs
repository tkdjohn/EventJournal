namespace EventJournal.Common.Configuration {
    public enum DatabaseProvider {
        Sqlite,
        MySql,
        SqlServer,
        PostgreSQL
    }
    public class DatabaseSettings : IServiceSettings {
        public static string ConfigurationSectionName => "Database";
        public string ConnectionString { get; set; } = null!;
        public DatabaseProvider Provider { get; set; } = DatabaseProvider.Sqlite;
        public bool UseDefaultConnectionString => string.IsNullOrWhiteSpace(ConnectionString) || ConnectionString.Equals("default", StringComparison.CurrentCultureIgnoreCase);

    }
}
