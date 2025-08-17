using EventJournal.DomainEntities;

namespace EventJournal.Data {
    public interface IEventRepository : IBaseRepository<Event> {
    }
}