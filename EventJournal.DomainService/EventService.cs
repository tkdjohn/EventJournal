using EventJournal.Data;
using EventJournal.Data.Entities;

namespace EventJournal.DomainService {
    public class EventService(
        IEventRepository eventRepository, 
        IDetailRepository detailRepository) : IEventService {
        //TODO: refactor to use a generic repository interface if possible
        //TODO: refactor to use models(DTOs) instead of entities

        public Task<IEnumerable<Event>> GetAllEventsAsync() {
            return eventRepository.GetAllAsync();
        }
        public Task<Event?> GetEventByIdAsync(Guid resourceId) {
            return eventRepository.GetByResourceIdAsync(resourceId);
        }
        public Task AddUpdateEventAsync(Event updatedEvent) {
            return eventRepository.AddUpdateAsync(updatedEvent);
        }
        public Task DeleteEventAsync(Event entity) {
            return eventRepository.DeleteAsync(entity);
        }

        public Task<IEnumerable<Detail>> GetAllDetailsAsync() {
            return detailRepository.GetAllAsync();
        }
        public Task<Detail?> GetDetailByIdAsync(Guid resourceId) {
            return detailRepository.GetByResourceIdAsync(resourceId);
        }
        public Task AddUpdateDetailAsync(Detail updatedDetail) {
            return detailRepository.AddUpdateAsync(updatedDetail);
        }
        public Task DeleteDetailAsync(Detail entity) {
            return detailRepository.DeleteAsync(entity);
        }
    }
}
