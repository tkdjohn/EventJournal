using EventJournal.DomainEntities;
using Microsoft.EntityFrameworkCore;

namespace EventJournal.Data {
    public abstract class BaseRepository<T>(IDatabaseContext db, DbSet<T> table) where T : BaseEntity{
        private readonly IDatabaseContext db = db;
        private readonly DbSet<T> table = table;
        public async Task<T> AddAsync(T entity) {
            var row = await table.AddAsync(entity).ConfigureAwait(false);
            await db.SaveChangesAsync().ConfigureAwait(false);
            return row.Entity;
        }
        public async Task RemoveAsync(T entity) {
            table.Remove(entity);
            await db.SaveChangesAsync().ConfigureAwait(false);
        }
        public async Task<T?> GetAsync(int id) {
            return await table.FindAsync(id).ConfigureAwait(false);
        }

        public async Task<T?> GetAsync(Guid resourceId) { 
            return await table.FirstOrDefaultAsync(t => t.ResourceId == resourceId).ConfigureAwait(false); 
        }

        // TODO: this seems like it could/should be an extension method
        public async Task<T> SaveEntity(T source) {
            ArgumentException.ThrowIfNullOrEmpty(nameof(source));
            var existingEntity = await GetAsync(source.Id)
                ?? await GetAsync(source.ResourceId);
            if (existingEntity == null) {
                return await AddAsync(source).ConfigureAwait(false);
            }
            
            existingEntity.UpdateEntity(source);

            await db.SaveChangesAsync().ConfigureAwait(false);
            return existingEntity;  
        }
    }
}
