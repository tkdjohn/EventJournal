using EventJournal.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventJournal.Data {
    public abstract class BaseRepository<T>(IDatabaseContext db, DbSet<T> table) : IBaseRepository<T> where T : BaseEntity {
        private readonly IDatabaseContext db = db;
        internal readonly DbSet<T> table = table;
        internal virtual async Task<T> AddAsync(T entity) {
            var row = await table.AddAsync(entity).ConfigureAwait(false);
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

        public virtual void Delete(T entity) {
            table.Remove(entity);
        }

        public virtual async Task<T> AddUpdateAsync(T source) {
            ArgumentException.ThrowIfNullOrEmpty(nameof(source));
            var entity = await GetByIdAsync(source.Id) ?? await GetByResourceIdAsync(source.ResourceId).ConfigureAwait(false);
            if (entity == null) {
                entity = await AddAsync(source).ConfigureAwait(false);
            } else {
                entity.UpdateEntity(source);
            }
            return entity;
        }

        public virtual async Task<IEnumerable<T>> AddUpdateAsync(IEnumerable<T> sources) {
            ArgumentException.ThrowIfNullOrEmpty(nameof(sources));
            var result = new List<T>();
            foreach (var source in sources) {
                var updatedEntity = await AddUpdateAsync(source).ConfigureAwait(false);
                result.Add(updatedEntity);
            }
            return result;
        }

        public Task SaveChangesAsync() {
            return db.SaveChangesAsync();
        }
    }
}
