using EventJournal.Data.Entities.UserTypes;
using Microsoft.EntityFrameworkCore;

namespace EventJournal.Data.UserTypeRepositories {
    public class DetailTypeRepository(IDatabaseContext db) : BaseRepository<DetailType>(db, db.DetailTypes), IDetailTypeRepository {
        public override async Task<IList<DetailType>> GetAllAsync() {
            return await table
                .Include(dt => dt.AllowedIntensities)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public override Task<DetailType?> GetByResourceIdAsync(Guid resourceId) {
            return table
                .Include(dt => dt.AllowedIntensities)
                .FirstOrDefaultAsync(dt => dt.ResourceId == resourceId);
        }
    }
}
