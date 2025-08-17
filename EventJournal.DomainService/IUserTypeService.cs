using EventJournal.Data.Entities.UserTypes;

namespace EventJournal.DomainService {
    public interface IUserTypeService {
        Task<DetailType> AddUpdateDetailTypeAsync(DetailType updatedDetailType);
        Task<EventType> AddUpdateEventTypeAsync(EventType updatedEventType);
        Task DeleteDetailTypeAsync(DetailType entity);
        Task DeleteEventTypeAsync(EventType entity);
        Task DeleteIntensityAsync(Intensity entity);
        Task<IEnumerable<DetailType>> GetAllDetailTypesAsync();
        Task<IEnumerable<EventType>> GetAllEventTypesAsync();
        Task<IEnumerable<Intensity>> GetAllIntensitiesAsync();
        Task<DetailType?> GetDetailTypeByIdAsync(Guid resourceId);
        Task<EventType?> GetEventTypeByIdAsync(Guid resourceId);
        Task<Intensity?> GetIntensityByIdAsync(Guid resourceId);
        Task<Intensity> UpdateIntensityAsync(Intensity updatedIntensity);
    }
}