using EventJournal.DomainDto.UserTypes;

namespace EventJournal.DomainService {
    public interface IUserTypeService {


        Task<IList<DetailTypeDto>> GetAllDetailTypesAsync();
        Task<DetailTypeDto?> GetDetailTypeByIdAsync(Guid resourceId);
        Task<DetailTypeDto> AddUpdateDetailTypeAsync(DetailTypeDto dto);
        Task DeleteDetailTypeAsync(Guid resourceId);

        Task<IList<EventTypeDto>> GetAllEventTypesAsync();
        Task<EventTypeDto?> GetEventTypeByIdAsync(Guid resourceId);
        Task<EventTypeDto> AddUpdateEventTypeAsync(EventTypeDto dto);
        Task DeleteEventTypeAsync(Guid resourceId);

        Task<IList<IntensityDto>> GetAllIntensitiesAsync();
        Task<IntensityDto?> GetIntensityByIdAsync(Guid resourceId);
        Task<IntensityDto> AddUpdateIntensityAsync(IntensityDto dto);
        Task DeleteIntensityAsync(Guid resourceId);
    }
}