using EventJournal.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventJournal.Data {
    public abstract class BaseRepository<T>(IDatabaseContext db, DbSet<T> table) : IBaseRepository<T> where T : BaseEntity {
        private readonly IDatabaseContext db = db;
        private readonly DbSet<T> table = table;
        internal async Task<T> AddAsync(T entity) {
            var row = await table.AddAsync(entity).ConfigureAwait(false);
            await db.SaveChangesAsync().ConfigureAwait(false);
            return row.Entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync() {
            return await table.ToListAsync().ConfigureAwait(false);
        }

        internal async Task<T?> GetByIdAsync(int id) {
            return await table.FindAsync(id).ConfigureAwait(false);
        }

        public Task<T?> GetByResourceIdAsync(Guid resourceId) {
            return table.FirstOrDefaultAsync(t => t.ResourceId == resourceId);
        }

        public Task DeleteAsync(T entity) {
            table.Remove(entity);
            return db.SaveChangesAsync();
        }

        public async Task<T> AddUpdateAsync(T source) {
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
