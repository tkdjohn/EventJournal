using EventJournal.DomainEntities.UserTypes;

namespace EventJournal.Data.UserTypeRepositories {
    public class EventTypeRepository(IDatabaseContext db) : BaseRepository<EventType>(db, db.EventTypes) {
    }
}
