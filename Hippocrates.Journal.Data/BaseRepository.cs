using Hippocrates.Journal.DomainEntities;
using Microsoft.EntityFrameworkCore;

namespace Hippocrates.Journal.Data {
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


        /// <summary>
        /// Copy values of one entity to another, to be implemented by descendants. 
        /// Recommend making use of automapper or some such. Note that CopyEntity should NOT
        /// set any of the BaseEntity values. Especially Id and ResourceId.
        /// </summary>
        /// <param name="destinationEntity"></param>
        /// <param name="sourceEntity"></param>
        protected abstract void CopyEntity(T destinationEntity, T sourceEntity);

        public async Task<T> UpdateAsync(T destinationEntity) {
            ArgumentException.ThrowIfNullOrEmpty(nameof(destinationEntity));
            var existingEntity = await GetAsync(destinationEntity.Id)
                ?? await GetAsync(destinationEntity.ResourceId);
            if (existingEntity == null) {
                return await AddAsync(destinationEntity).ConfigureAwait(false);
            }
            
            int id = existingEntity.Id;
            Guid resourceId = existingEntity.ResourceId;
            CopyEntity(destinationEntity, existingEntity);
            
            // set updated date and ensure CopyEntity implementations don't reset id or resource id
            existingEntity.UpdatedDate = DateTime.UtcNow;
            existingEntity.Id = id;
            existingEntity.ResourceId = resourceId;

            await db.SaveChangesAsync().ConfigureAwait(false);
            return existingEntity;  
        }
    }
}
