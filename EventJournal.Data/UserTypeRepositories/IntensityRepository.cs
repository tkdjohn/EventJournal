using EventJournal.Data.Entities.UserTypes;

namespace EventJournal.Data.UserTypeRepositories {
    public class IntensityRepository(IDatabaseContext db) : BaseRepository<Intensity>(db, db.Intensities), IIntensityRepository {

    }
}
