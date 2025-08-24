using EventJournal.Data.Entities.UserTypes;
using Microsoft.EntityFrameworkCore;

namespace EventJournal.Data.UserTypeRepositories {
    public class IntensityRepository(IDatabaseContext db) : BaseRepository<Intensity>(db, db.Intensities), IIntensityRepository {
        public override async Task<IList<Intensity>> GetAllAsync() {
            return await table
                .OrderBy(i => i.Level)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public override Task<Intensity?> GetByResourceIdAsync(Guid resourceId) {
            return table
                .FirstOrDefaultAsync(i => i.ResourceId == resourceId);
        }
    }
}
