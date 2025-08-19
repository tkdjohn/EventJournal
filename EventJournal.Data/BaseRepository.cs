using EventJournal.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventJournal.Data {
    public abstract class BaseRepository<T>(IDatabaseContext db, DbSet<T> table) : IBaseRepository<T> where T : BaseEntity {
        private readonly IDatabaseContext db = db;
        internal readonly DbSet<T> table = table;
        internal virtual async Task<T> AddAsync(T entity) {
            var row = await table.AddAsync(entity).ConfigureAwait(false);
            await db.SaveChangesAsync().ConfigureAwait(false);
            return row.Entity;
        }

        public virtual async Task<IList<T>> GetAllAsync() {
            return await table.ToListAsync().ConfigureAwait(false);
        }

        internal virtual async Task<T?> GetByIdAsync(int id) {
            return await table.FindAsync(id).ConfigureAwait(false);
        }

        public virtual Task<T?> GetByResourceIdAsync(Guid resourceId) {
            return table.FirstOrDefaultAsync(t => t.ResourceId == resourceId);
        }
                
        public virtual Task DeleteAsync(T entity) {
            table.Remove(entity);
            return db.SaveChangesAsync();
        }

        public virtual async Task<T> AddUpdateAsync(T source) {
            ArgumentException.ThrowIfNullOrEmpty(nameof(source));
            var existingEntity = await GetByIdAsync(source.Id)
                ?? await GetByResourceIdAsync(source.ResourceId).ConfigureAwait(false);
            if (existingEntity == null) {
                return await AddAsync(source).ConfigureAwait(false);
            }

            existingEntity.UpdateEntity(source);

            await db.SaveChangesAsync().ConfigureAwait(false);
            return existingEntity;
        }
    }
}
