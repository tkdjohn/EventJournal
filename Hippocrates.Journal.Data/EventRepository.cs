using EventJournal.DomainEntities;

namespace EventJournal.Data {
    public class EventRepository(IDatabaseContext db) : BaseRepository<Event>(db, db.Events) {
    }
}
