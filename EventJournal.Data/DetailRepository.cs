using EventJournal.DomainEntities;

namespace EventJournal.Data {
    public class DetailRepository(IDatabaseContext db) : BaseRepository<Detail>(db, db.Details) {
    }
}
