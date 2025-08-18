using AutoMapper;
using EventJournal.Data;
using EventJournal.Data.Entities;
using EventJournal.DomainDto;
using EventJournal.DomainService.Exceptions;
using System.Linq;

namespace EventJournal.DomainService {
    public class EventService(
        IEventRepository eventRepository,
        IDetailRepository detailRepository,
        IMapper mapper) : IEventService {

        public async Task<IList<EventDto>> GetAllEventsAsync() {
            return mapper.Map<IList<EventDto>>(await eventRepository.GetAllAsync().ConfigureAwait(false));
        }
        public async Task<EventDto?> GetEventByIdAsync(Guid resourceId) {
            return mapper.Map<EventDto?>(await eventRepository.GetByResourceIdAsync(resourceId).ConfigureAwait(false));
        }
        public async Task<EventDto> AddUpdateEventAsync(EventDto dto) {
            ArgumentNullException.ThrowIfNull(dto);
            //TODO: additional validations?
            return mapper.Map<EventDto>(await eventRepository.AddUpdateAsync(mapper.Map<Event>(dto)).ConfigureAwait(false));
        }
        public async Task DeleteEventAsync(Guid resourceId) {
            var entity = await eventRepository.GetByResourceIdAsync(resourceId).ConfigureAwait(false);
            ResourceNotFoundException.ThrowIfNull(entity, $"Event with resource id {resourceId} not found");
            await eventRepository.DeleteAsync(entity).ConfigureAwait(false);
        }

        public async Task<IList<DetailDto>> GetAllDetailsAsync() {
            return mapper.Map<IList<DetailDto>>(await detailRepository.GetAllAsync().ConfigureAwait(false));
        }
        public async Task<DetailDto?> GetDetailByIdAsync(Guid resourceId) {
            return mapper.Map<DetailDto?>(await detailRepository.GetByResourceIdAsync(resourceId).ConfigureAwait(false));
        }
        public async Task<DetailDto> AddUpdateDetailAsync(DetailDto dto) {
            ArgumentNullException.ThrowIfNull(dto);
            //TODO: additional validations?
            return mapper.Map<DetailDto>(await detailRepository.AddUpdateAsync(mapper.Map<Detail>(dto)).ConfigureAwait(false));
        }
        public async Task DeleteDetailAsync(Guid resourceId) {
            var entity = await detailRepository.GetByResourceIdAsync(resourceId).ConfigureAwait(false);
            ResourceNotFoundException.ThrowIfNull(entity, $"Event with resource id {resourceId} not found");
            await detailRepository.DeleteAsync(entity).ConfigureAwait(false);
        }
    }
}
