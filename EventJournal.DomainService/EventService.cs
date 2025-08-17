using EventJournal.Data;
using EventJournal.DomainEntities;

namespace EventJournal.DomainService {
    public class EventService(
        IEventRepository eventRepository, 
        IDetailRepository detailRepository) : IEventService {
        //TODO: refactor to use a generic repository interface if possible
        //TODO: refactor to use models(DTOs) instead of entities

        public async Task<IEnumerable<Event>> GetAllEventsAsync() {
            return await eventRepository.GetAllAsync();
        }
        public async Task<Event?> GetEventByIdAsync(Guid resourceId) {
            return await eventRepository.GetByResourceIdAsync(resourceId);
        }
        public async Task AddUpdateEventAsync(Event updatedEvent) {
            await eventRepository.AddUpdateAsync(updatedEvent);
        }
        public async Task DeleteEventAsync(Event entity) {
            await eventRepository.DeleteAsync(entity);
        }

        public async Task<IEnumerable<Detail>> GetAllDetailsAsync() {
            return await detailRepository.GetAllAsync();
        }
        public async Task<Detail?> GetDetailByIdAsync(Guid resourceId) {
            return await detailRepository.GetByResourceIdAsync(resourceId);
        }
        public async Task AddUpdateDetailAsync(Detail updatedDetail) {
            await detailRepository.AddUpdateAsync(updatedDetail);
        }
        public async Task DeleteDetailAsync(Detail entity) {
            await detailRepository.DeleteAsync(entity);
        }
    }
}
