using EventJournal.DomainDto;

namespace EventJournal.DomainService {
    public interface IEventService {
        Task<IEnumerable<EventDto>> GetAllEventsAsync();
        Task<EventDto?> GetEventByIdAsync(Guid resourceId);
        Task<EventDto> AddUpdateEventAsync(EventDto dto);
        Task DeleteEventAsync(Guid resourceId);

        Task<IEnumerable<DetailDto>> GetAllDetailsAsync();
        Task<DetailDto?> GetDetailByIdAsync(Guid resourceId);
        Task<DetailDto> AddUpdateDetailAsync(DetailDto dto);
        Task DeleteDetailAsync(Guid resourceId);
    }
}