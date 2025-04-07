using Hippocrates.Journal.DomainEntities;
using Microsoft.EntityFrameworkCore;

namespace Hippocrates.Journal.Data {

    public class DatabaseContext : DbContext, IDatabaseContext {
        public DbSet<Symptom> Symptoms { get; set; } = null!;
        public DbSet<IntensityReposity> Intensities { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<EventTypeRepository> EventTypes { get; set; } = null!;
        public DbSet<EventSymptomRepository> EventSymptoms { get; set; } = null!;

        public DatabaseContext() {
            // needed by EF cli tools
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {
            // needed to properly inject DBContext at runtime

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
            return Path.Join(path, "PetShop.db");
        }

        public Task SaveChangesAsync() {
            return base.SaveChangesAsync();
        }
    }
}
