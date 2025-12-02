using EventJournal.Common.Configuration;
using EventJournal.Data.Entities;
using EventJournal.Data.Entities.UserTypes;
using Microsoft.EntityFrameworkCore;

namespace EventJournal.Data {

    public class DatabaseContext : DbContext, IDatabaseContext {
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<EventType> EventTypes { get; set; } = null!;
        public DbSet<Detail> Details { get; set; } = null!;
        public DbSet<DetailType> DetailTypes { get; set; } = null!;
        public DbSet<Intensity> Intensities { get; set; } = null!;

        public DatabaseContext() {
            // needed by EF cli tools 
            // TODO: is it needed if we have the DesignTimeDbContextFactory?
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {
            // needed to properly inject DBContext at runtime
        }
        // TODO: do we need enumeration conversion?
        //protected override void ConfigureConventions(ModelConfigurationBuilder builder) {
        //    // Applies conversion to all enumerations
        //    _ = builder.Properties<Enum>()
        //        .HaveConversion<EnumToStringConverter<Enum>>()
        //        .HaveColumnType("nvarchar(50)"); 
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder
                .Entity<DetailType>()
                .Property(e => e.IntensitySortType)
                .HasConversion<string>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            //TODO: this isn't working as expected, options isn't the one created in the installer
            if (!options.IsConfigured) {
                base.OnConfiguring(options);
                // options.UseSqlite(GetConnectionString());
            }
        }

        public static DbContextOptionsBuilder ConfigureFromSettings(DbContextOptionsBuilder options, DatabaseSettings? settings = null) {
            settings ??= DefaultSettings;
            switch (settings.Provider) {
                default:
                case DatabaseProvider.Sqlite:
                    options.UseSqlite(GetConnectionString(settings));
                    break;
                    //case DatabaseProvider.MySql:
                    //    optionsBuilder.UseMySql(GetConnectionString(settings), ServerVersion.AutoDetect(GetConnectionString(settings)));
                    //    break;
                    //case DatabaseProvider.SqlServer:
                    //    optionsBuilder.UseSqlServer(GetConnectionString(settings));
                    //    break;
                    //case DatabaseProvider.PostgreSQL:
                    //    optionsBuilder.UseNpgsql(GetConnectionString(settings));
                    //    break;
            }
            return options;
        }
        private static DatabaseSettings DefaultSettings { get; set; } = new DatabaseSettings() {
            ConnectionString = string.Empty,
            Provider = DatabaseProvider.Sqlite
        };

        // TODO: make private or internal after we get DI for distributed lock supporting multiple DB providers
        public static string GetConnectionString(DatabaseSettings? settings = null) {

            settings ??= DefaultSettings;

            switch (settings.Provider) {
                default:
                case DatabaseProvider.Sqlite:
                    if (settings.UseDefaultConnectionString) {
                        // The following configures EF to create a Sqlite database file in the
                        // special "local" folder for your platform.
                        var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                        var program = System.Reflection.Assembly.GetEntryAssembly()?.GetName().Name!.Split(".")[0];
                        return $"Data Source={Path.Join(path, $"{program}.db")}";
                    }
                    break;
                    //case DatabaseProvider.SqlServer:
                    //break;
                    //case DatabaseProvider.PostgreSQL:
                    //break;
            }
            return settings.ConnectionString;
        }

        public Task SaveChangesAsync() {
            return base.SaveChangesAsync();
        }
    }
}
