using EventJournal.Data.Entities.UserTypes;
using EventJournal.Data.UserTypeRepositories;

namespace EventJournal.DomainService {
    public class UserTypeService(
        IDetailTypeRepository detailRepository,
        IEventTypeRepository eventTypeRepository,
        IIntensityRepository intensityRepository) : IUserTypeService {
        //TODO: refactor to use a generic repository interface if possible
        //TODO: refactor to use models(DTOs) instead of entities

        public Task<IEnumerable<DetailType>> GetAllDetailTypesAsync() {
            return detailRepository.GetAllAsync();
        }
        public Task<DetailType?> GetDetailTypeByIdAsync(Guid resourceId) {
            return detailRepository.GetByResourceIdAsync(resourceId);
        }
        public Task<DetailType> AddUpdateDetailTypeAsync(DetailType updatedDetailType) {
            return detailRepository.AddUpdateAsync(updatedDetailType);
        }
        public Task DeleteDetailTypeAsync(DetailType entity) {
            return detailRepository.DeleteAsync(entity);
        }

        public Task<IEnumerable<EventType>> GetAllEventTypesAsync() {
            return eventTypeRepository.GetAllAsync();
        }
        public Task<EventType?> GetEventTypeByIdAsync(Guid resourceId) {
            return eventTypeRepository.GetByResourceIdAsync(resourceId);
        }
        public Task<EventType> AddUpdateEventTypeAsync(EventType updatedEventType) {
            return eventTypeRepository.AddUpdateAsync(updatedEventType);
        }
        public Task DeleteEventTypeAsync(EventType entity) {
            return eventTypeRepository.DeleteAsync(entity);
        }

        public Task<IEnumerable<Intensity>> GetAllIntensitiesAsync() {
            return intensityRepository.GetAllAsync();
        }
        public Task<Intensity?> GetIntensityByIdAsync(Guid resourceId) {
            return intensityRepository.GetByResourceIdAsync(resourceId);
        }
        public Task<Intensity> UpdateIntensityAsync(Intensity updatedIntensity) {
            return intensityRepository.AddUpdateAsync(updatedIntensity);
        }
        public Task DeleteIntensityAsync(Intensity entity) {
            return intensityRepository.DeleteAsync(entity);
        }
    }
}
