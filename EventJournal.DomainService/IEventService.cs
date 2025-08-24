using EventJournal.DomainDto;
using EventJournal.DomainDto.UserTypes;

namespace EventJournal.DomainService {
    public interface IEventService {
        // ======================> Events <======================
        Task<IList<EventDto>> GetAllEventsAsync();
        Task<EventDto?> GetEventByIdAsync(Guid resourceId);
        Task<EventDto> AddUpdateEventAsync(EventDto dto);
        Task<IEnumerable<EventDto>> AddUpdateEventsAsync(IEnumerable<EventDto> dtos);
        Task DeleteEventAsync(Guid resourceId);

        // ======================> Event Types <======================
        Task<IList<EventTypeDto>> GetAllEventTypesAsync();
        Task<EventTypeDto?> GetEventTypeByIdAsync(Guid resourceId);
        Task<EventTypeDto> AddUpdateEventTypeAsync(EventTypeDto dto);
        Task<IEnumerable<EventTypeDto>> AddUpdateEventTypesAsync(IEnumerable<EventTypeDto> dtos);
        Task DeleteEventTypeAsync(Guid resourceId);

        Task AddTestDataAsync();
    }
}