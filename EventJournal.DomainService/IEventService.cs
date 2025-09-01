using EventJournal.Data.Entities;
using EventJournal.DomainDto;

namespace EventJournal.DomainService {
    public interface IEventService {
        // ======================> Events <======================
        Task<IList<EventDto>> GetAllEventsAsync();
        Task<EventDto?> GetEventByIdAsync(Guid resourceId);
        Task<EventDto> AddUpdateEventAsync(EventDto dto);
        Task<IEnumerable<EventDto>> AddUpdateEventsAsync(IEnumerable<EventDto> dtos);
        Task DeleteEventAsync(Guid resourceId);
        
        Task<DetailDto> AddUpdateDetailAsync(Guid eventResourceId, DetailDto detailDto);
        Task AddUpdateDetailsAsync(Guid eventResourceId, IEnumerable<DetailDto> detailDtos);
        Task RemoveDetailAsync(Guid eventResourceId, Guid detailResourceId);
        Task RemoveAllDetailsAsync(Guid eventResourceId);
    }
}