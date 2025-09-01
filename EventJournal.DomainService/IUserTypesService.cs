using EventJournal.DomainDto.UserTypes;

namespace EventJournal.DomainService {
    public interface IUserTypesService {


        // ======================> Event Types <======================
        Task<IList<EventTypeDto>> GetAllEventTypesAsync();
        Task<EventTypeDto?> GetEventTypeByIdAsync(Guid resourceId);
        Task<EventTypeDto> AddUpdateEventTypeAsync(EventTypeDto dto);
        Task<IEnumerable<EventTypeDto>> AddUpdateEventTypesAsync(IEnumerable<EventTypeDto> dtos);
        Task DeleteEventTypeAsync(Guid resourceId);

        // ======================> Detail Types <======================
        Task<IList<DetailTypeDto>> GetAllDetailTypesAsync();
        Task<DetailTypeDto?> GetDetailTypeByIdAsync(Guid resourceId);
        Task<DetailTypeDto> AddUpdateDetailTypeAsync(DetailTypeDto dto);
        Task<IEnumerable<DetailTypeDto>> AddUpdateDetailTypesAsync(IEnumerable<DetailTypeDto> dtos);
        Task DeleteDetailTypeAsync(Guid resourceId);
        Task<IntensityDto> AddUpdateAllowedIntensityAsync(Guid detailTypeResourceId, IntensityDto intensityDto);
        Task AddUpdateAllowedIntensitiesAsync(Guid detailTypeResourceId, IEnumerable<IntensityDto> intensityDtos);
        Task RemoveAllowedIntensityAsync(Guid detailTypeResourceId, Guid intensityResourceId);
        Task RemoveAllAllowedIntensitiesAsync(Guid detailTypeResourceId);
    }
}