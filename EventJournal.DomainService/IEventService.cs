using EventJournal.DomainEntities;

namespace EventJournal.DomainService {
    public interface IEventService {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event?> GetEventByIdAsync(Guid resourceId);
        Task AddUpdateEventAsync(Event updatedEvent);
        Task DeleteEventAsync(Event entity);

        Task<IEnumerable<Detail>> GetAllDetailsAsync();
        Task<Detail?> GetDetailByIdAsync(Guid resourceId);
        Task AddUpdateDetailAsync(Detail updatedDetail);
        Task DeleteDetailAsync(Detail entity);
    }
}