using AutoMapper;
using EventJournal.Data;
using EventJournal.Data.Entities;
using EventJournal.Data.Entities.UserTypes;
using EventJournal.Data.UserTypeRepositories;
using EventJournal.DomainDto;
using EventJournal.DomainDto.UserTypes;

namespace EventJournal.DomainService {
    public class EventService(
        IEventRepository eventRepository,
        IEventTypeRepository eventTypeRepository,
        IMapper mapper)
    : IEventService {

        // ======================> Events <======================
        public async Task<IList<EventDto>> GetAllEventsAsync() {
            return mapper.Map<IList<EventDto>>(await eventRepository.GetAllAsync().ConfigureAwait(false));
        }

        public async Task<EventDto?> GetEventByIdAsync(Guid resourceId) {
            return mapper.Map<EventDto?>(await eventRepository.GetByResourceIdAsync(resourceId).ConfigureAwait(false));
        }

        public async Task<EventDto> AddUpdateEventAsync(EventDto dto) {
            ArgumentNullException.ThrowIfNull(dto);
            //TODO: additional validations?
            var eventEntity = await eventRepository.AddUpdateAsync(mapper.Map<Event>(dto)).ConfigureAwait(false);
            await eventRepository.SaveChangesAsync().ConfigureAwait(false);
            return mapper.Map<EventDto>(eventEntity);

        }

        public async Task<IEnumerable<EventDto>> AddUpdateEventsAsync(IEnumerable<EventDto> dtos) {
            ArgumentNullException.ThrowIfNull(dtos);
            var eventEntities = await eventRepository.AddUpdateAsync(mapper.Map<IEnumerable<Event>>(dtos)).ConfigureAwait(false);
            await eventRepository.SaveChangesAsync().ConfigureAwait(false);
            return mapper.Map<List<EventDto>>(eventEntities);
        }

        public async Task DeleteEventAsync(Guid resourceId) {
            var entity = await eventRepository.GetByResourceIdAsync(resourceId).ConfigureAwait(false);
            if (entity == null) {
                return;
            }
            eventRepository.Delete(entity);
            await eventRepository.SaveChangesAsync().ConfigureAwait(false);
        }

        // ======================> Event Types <======================
        public async Task<IList<EventTypeDto>> GetAllEventTypesAsync() {
            return mapper.Map<IList<EventTypeDto>>(await eventTypeRepository.GetAllAsync().ConfigureAwait(false));
        }

        public async Task<EventTypeDto?> GetEventTypeByIdAsync(Guid resourceId) {
            return mapper.Map<EventTypeDto?>(await eventTypeRepository.GetByResourceIdAsync(resourceId).ConfigureAwait(false));
        }

        public async Task<EventTypeDto> AddUpdateEventTypeAsync(EventTypeDto dto) {
            ArgumentNullException.ThrowIfNull(dto);
            //TODO: additional validations?
            var eventTypeEntity = await eventTypeRepository.AddUpdateAsync(mapper.Map<EventType>(dto)).ConfigureAwait(false);
            await eventTypeRepository.SaveChangesAsync().ConfigureAwait(false);
            return mapper.Map<EventTypeDto>(eventTypeEntity);
        }

        public async Task<IEnumerable<EventTypeDto>> AddUpdateEventTypesAsync(IEnumerable<EventTypeDto> dtos) {
            ArgumentNullException.ThrowIfNull(dtos);
            var eventTypeEntities = await eventTypeRepository.AddUpdateAsync(mapper.Map<IEnumerable<EventType>>(dtos)).ConfigureAwait(false);
            await eventTypeRepository.SaveChangesAsync().ConfigureAwait(false);
            return mapper.Map<List<EventTypeDto>>(eventTypeEntities);
        }

        public async Task DeleteEventTypeAsync(Guid resourceId) {
            var entity = await eventTypeRepository.GetByResourceIdAsync(resourceId).ConfigureAwait(false);
            if (entity == null) {
                return;
            }
            eventTypeRepository.Delete(entity);
            await eventTypeRepository.SaveChangesAsync().ConfigureAwait(false);
        }


        public async Task AddTestDataAsync() {
            await AddUpdateEventTypesAsync(EventTypeDto.DefaultEventTypeDtos).ConfigureAwait(false);
            await AddUpdateEventAsync(EventDto.DefaultEventDto);
        }
    }
}
