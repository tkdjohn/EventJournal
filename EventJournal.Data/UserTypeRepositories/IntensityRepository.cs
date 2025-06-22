using EventJournal.DomainEntities;
using EventJournal.DomainEntities.UserTypes;

namespace EventJournal.Data.UserTypeRepositories {
    public class IntensityRepository(IDatabaseContext db) : BaseRepository<Intensity>(db, db.Intensities) {

    }
}
