using EventJournal.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventJournal.Data {
    public class DetailRepository(IDatabaseContext db) : BaseRepository<Detail>(db, db.Details), IDetailRepository {
        public override async Task<IList<Detail>> GetAllAsync() {
            return await table
                .Include(d => d.DetailType)
                    .ThenInclude(dt => dt.AllowedIntensities)
                .Include(d => d.Intensity)
                .ToListAsync()
                .ConfigureAwait(false);
        }
        public override Task<Detail?> GetByResourceIdAsync(Guid resourceId) {
            return table
                .Include(d => d.DetailType)
                    .ThenInclude(dt => dt.AllowedIntensities)
                .Include(d => d.Intensity)
                .FirstOrDefaultAsync(d => d.ResourceId == resourceId);
        }
    }
}
