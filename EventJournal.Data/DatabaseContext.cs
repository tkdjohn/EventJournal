using EventJournal.Data.Entities;
using EventJournal.Data.Entities.UserTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
                options.UseSqlite($"Data Source={GetSqliteDbPath()}");
            }
        }

        public static string GetSqliteDbPath() {
            // The following configures EF to create a Sqlite database file in the
            // special "local" folder for your platform.
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            return Path.Join(path, "EventJournal.db");
        }

        public Task SaveChangesAsync() {
            return base.SaveChangesAsync();
        }
    }
}
