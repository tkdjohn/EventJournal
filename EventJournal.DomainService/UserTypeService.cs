using EventJournal.Data.UserTypeRepositories;
using EventJournal.DomainEntities.UserTypes;

namespace EventJournal.DomainService {
    public class UserTypeService(
        IDetailTypeRepository detailRepository,
        IEventTypeRepository eventTypeRepository,
        IIntensityRepository intensityRepository) : IUserTypeService {
        //TODO: refactor to use a generic repository interface if possible
        //TODO: refactor to use models(DTOs) instead of entities

        public async Task<IEnumerable<DetailType>> GetAllDetailTypesAsync() {
            return await detailRepository.GetAllAsync();
        }
        public async Task<DetailType?> GetDetailTypeByIdAsync(Guid resourceId) {
            return await detailRepository.GetByResourceIdAsync(resourceId);
        }
        public async Task<DetailType> AddUpdateDetailTypeAsync(DetailType updatedDetailType) {
            return await detailRepository.AddUpdateAsync(updatedDetailType);
        }
        public async Task DeleteDetailTypeAsync(DetailType entity) {
            await detailRepository.DeleteAsync(entity);
        }

        public async Task<IEnumerable<EventType>> GetAllEventTypesAsync() {
            return await eventTypeRepository.GetAllAsync();
        }
        public async Task<EventType?> GetEventTypeByIdAsync(Guid resourceId) {
            return await eventTypeRepository.GetByResourceIdAsync(resourceId);
        }
        public async Task<EventType> AddUpdateEventTypeAsync(EventType updatedEventType) {
            return await eventTypeRepository.AddUpdateAsync(updatedEventType);
        }
        public async Task DeleteEventTypeAsync(EventType entity) {
            await eventTypeRepository.DeleteAsync(entity);
        }

        public async Task<IEnumerable<Intensity>> GetAllIntensitiesAsync() {
            return await intensityRepository.GetAllAsync();
        }
        public async Task<Intensity?> GetIntensityByIdAsync(Guid resourceId) {
            return await intensityRepository.GetByResourceIdAsync(resourceId);
        }
        public async Task<Intensity> UpdateIntensityAsync(Intensity updatedIntensity) {
            return await intensityRepository.AddUpdateAsync(updatedIntensity);
        }
        public async Task DeleteIntensityAsync(Intensity entity) {
            await intensityRepository.DeleteAsync(entity);
        }
    }
}
