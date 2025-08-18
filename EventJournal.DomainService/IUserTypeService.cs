using EventJournal.DomainDto.UserTypes;

namespace EventJournal.DomainService {
    public interface IUserTypeService {


        Task<IEnumerable<DetailTypeDto>> GetAllDetailTypesAsync();
        Task<DetailTypeDto?> GetDetailTypeByIdAsync(Guid resourceId);
        Task<DetailTypeDto> AddUpdateDetailTypeAsync(DetailTypeDto dto);
        Task DeleteDetailTypeAsync(Guid resourceId);

        Task<IEnumerable<EventTypeDto>> GetAllEventTypesAsync();
        Task<EventTypeDto?> GetEventTypeByIdAsync(Guid resourceId);
        Task<EventTypeDto> AddUpdateEventTypeAsync(EventTypeDto dto);
        Task DeleteEventTypeAsync(Guid resourceId);

        Task<IEnumerable<IntensityDto>> GetAllIntensitiesAsync();
        Task<IntensityDto?> GetIntensityByIdAsync(Guid resourceId);
        Task<IntensityDto> AddUpdateIntensityAsync(IntensityDto dto);
        Task DeleteIntensityAsync(Guid resourceId);
    }
}