using EventJournal.Data.Entities.UserTypes;

namespace EventJournal.Data.UserTypeRepositories {
    public class EventTypeRepository(IDatabaseContext db) : BaseRepository<EventType>(db, db.EventTypes), IEventTypeRepository {
    }
}
