using Hippocrates.Journal.DomainEntities;

namespace Hippocrates.Journal.Data {
    public class EventRepository(IDatabaseContext db) : BaseRepository<Event>(db, db.Events) {
        public override Task UpdateAsync(Event entityUpdate) {
            //ArgumentNullException.ThrowIfNull(nameof(orderUpdate));
            //var existingOrder = await db.Orders.FindAsync(orderUpdate.OrderId).ConfigureAwait(false)
            //    ?? throw new InvalidOperationException($"Order Id {orderUpdate.OrderId} not found");

            //existingOrder.OrderDate = orderUpdate.OrderDate;
            //// TODO: this is actually bad and will likely leave orphaned OrderProducts
            //// in the database
            //existingOrder.OrderProducts = orderUpdate.OrderProducts;
            //existingOrder.LastUpdatedDate = DateTime.Now;
            //await db.SaveChangesAsync().ConfigureAwait(false);
            throw new NotImplementedException();
        }
    }
}
