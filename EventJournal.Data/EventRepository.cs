using EventJournal.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventJournal.Data {
    public sealed class EventRepository(IDatabaseContext db) : BaseRepository<Event>(db, db.Events), IEventRepository {
        public override async Task<IList<Event>> GetAllAsync() {
            return await table
                .Include(e => e.EventType)
                .Include(e => e.Details)
                    .ThenInclude(d => d.DetailType)
                        .ThenInclude(dt => dt.AllowedIntensities)
                .Include(e => e.Details)
                    .ThenInclude(d => d.Intensity)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public override Task<Event?> GetByResourceIdAsync(Guid resourceId) {
            return table
                .Include(e => e.EventType)
                .Include(e => e.Details)
                    .ThenInclude(d => d.DetailType)
                .Include(e => e.Details)
                    .ThenInclude(d => d.Intensity)
                .FirstOrDefaultAsync(e => e.ResourceId == resourceId);
        }
    }
}
