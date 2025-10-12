using EventJournal.Configuration;
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
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {
            // needed to properly inject DBContext at runtime
        }

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
            if (!options.IsConfigured) {
                options.UseSqlite(GetConnectionString());
            }
        }

        public static string GetConnectionString(DatabaseSettings? settings = null) {

            settings ??= new DatabaseSettings() {
                ConnectionString = string.Empty,
                DefaultProvider = DatabaseProvider.Sqlite
            };

            switch (settings.DefaultProvider) {
                default:
                case DatabaseProvider.Sqlite:
                    if (settings.UseDefaultConnectionString) {
                        // The following configures EF to create a Sqlite database file in the
                        // special "local" folder for your platform.
                        var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                        return $"Data Source={Path.Join(path, "EventJournal.db")}";
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
