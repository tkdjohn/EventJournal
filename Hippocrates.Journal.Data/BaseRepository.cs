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
            return await table.FirstOrDefaultAsync(t => t.Id  == id).ConfigureAwait(false);
        }

        public async Task<T?> GetAsync(Guid resourceId) { 
            return await table.FirstOrDefaultAsync(t => t.ResourceId == resourceId).ConfigureAwait(false); 
        }

        //TODO: can this handle created date and lastmodified
        //date and prevent updates to id and resourceid
        // and then call a delegate? -- look and see how cortside handles this
        public abstract Task UpdateAsync(T entityUpdate);
        //public override Task UpdateAsync(Event entityUpdate) {
            //ArgumentNullException.ThrowIfNull(nameof(orderUpdate));
            //var existingOrder = await db.Orders.FindAsync(orderUpdate.OrderId).ConfigureAwait(false)
            //    ?? throw new InvalidOperationException($"Order Id {orderUpdate.OrderId} not found");

            //existingOrder.OrderDate = orderUpdate.OrderDate;
            //// TODO: this is actually bad and will likely leave orphaned OrderProducts
            //// in the database
            //existingOrder.OrderProducts = orderUpdate.OrderProducts;
            //existingOrder.LastUpdatedDate = DateTime.Now;
            //await db.SaveChangesAsync().ConfigureAwait(false);
            //throw new NotImplementedException();
        //}
    }
}
