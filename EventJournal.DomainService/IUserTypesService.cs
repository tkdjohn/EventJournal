using EventJournal.DomainDto;
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

        //  =======================> Intensities <======================
        Task<IList<IntensityDto>> GetAllIntensitiesAsync();
        Task<IntensityDto?> GetIntensityByIdAsync(Guid resourceId);
        Task<IntensityDto> AddUpdateIntensityAsync(IntensityDto dto);
        Task<IEnumerable<IntensityDto>> AddUpdateIntensitiesAsync(IEnumerable<IntensityDto> dtos);
        Task DeleteIntensityAsync(Guid resourceId);

        Task AddTestDataAsync();
    }
}