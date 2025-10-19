using EventJournal.Common.Configuration;
using EventJournal.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EventJournal.CLI {
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DatabaseContext> {
        public DatabaseContext CreateDbContext(string[] args) {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var dbSettings = configuration.GetSection(DatabaseSettings.ConfigurationSectionName).Get<DatabaseSettings>()
                ?? throw new InvalidOperationException("Configuration settings does not contain a valid DatabaseSettings section.");

            var builder = new DbContextOptionsBuilder<DatabaseContext>();
            DatabaseContext.ConfigureFromSettings(builder, dbSettings);

            return new DatabaseContext(builder.Options);
        }
    }
}
