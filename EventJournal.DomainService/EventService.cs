using AutoMapper;
using EventJournal.Data;
using EventJournal.Data.Entities;
using EventJournal.DomainDto;
using EventJournal.DomainService.Exceptions;

namespace EventJournal.DomainService {
    public class EventService(
        IEventRepository eventRepository,
        //TODO: fix this services shouldn't call services
        IUserTypesService userTypesService,
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
            var savedEntity = await AddUpdateEventPrivateAsync(mapper.Map<Event>(dto)).ConfigureAwait(false);
            // so the problem is that we need to find existing DetailType with it's existing AllowedIntensities and THEN assign detail to detail
            // and pick the right intensity. Not sure this should happen here maybe in AddUpdateDetailAsync() ??
            // - in other wrds extend the logic in AddUpdteEventPrivateAsync to utilize similar logic  that exists in the USErTypesServie for DetailType and Intensities.
            // /// remember the goal is that Detail selects a valid intensity from teh list of allowed intensities specified by detail type.
            // another stray thought: maybe we need a mapping table that maps Detail id and Intensity id?
            await eventRepository.SaveChangesAsync().ConfigureAwait(false);
            return mapper.Map<EventDto>(savedEntity);
        }
        private Task<Event> AddUpdateEventPrivateAsync(Event entity) {
            // don't duplicate details - match on either Id or ResourceId
            foreach (var detail in entity.Details) {
                detail.Event = entity;
                var existingDetail = entity.Details.FirstOrDefault(d => d.Id == detail.Id || d.ResourceId == detail.ResourceId);
                if (existingDetail != null) {
                    detail.Id = existingDetail.Id;
                    detail.ResourceId = existingDetail.ResourceId;
                }
            }

            return eventRepository.AddUpdateAsync(entity);
        }
        public async Task<IEnumerable<EventDto>> AddUpdateEventsAsync(IEnumerable<EventDto> dtos) {
            ArgumentNullException.ThrowIfNull(dtos);
            var events = new List<Event>();
            foreach (var dto in dtos) {
                Event @event = await AddUpdateEventPrivateAsync(mapper.Map<Event>(dto)).ConfigureAwait(false);
                events.Add(@event);
            }
            await eventRepository.SaveChangesAsync().ConfigureAwait(false);
            return mapper.Map<List<EventDto>>(events);
        }

        public async Task DeleteEventAsync(Guid resourceId) {
            var entity = await eventRepository.GetByResourceIdAsync(resourceId).ConfigureAwait(false);
            if (entity == null) {
                return;
            }
            //TODO: does this leave orphaned details?
            eventRepository.Delete(entity);
            await eventRepository.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<DetailDto> AddUpdateDetailAsync(Guid eventResourceId, DetailDto detailDto) {
            ArgumentNullException.ThrowIfNull(detailDto);
            var eventEntity = await eventRepository.GetByResourceIdAsync(eventResourceId).ConfigureAwait(false) ?? throw new ResourceNotFoundException($"Event with ResourceId {eventResourceId} not found.");
            var result = eventEntity.AddUpdateDetail(mapper.Map<Detail>(detailDto));
            await eventRepository.SaveChangesAsync().ConfigureAwait(false);
            return mapper.Map<DetailDto>(result);
        }

        public async Task AddUpdateDetailsAsync(Guid eventResourceId, IEnumerable<DetailDto> detailDtos) {
            ArgumentNullException.ThrowIfNull(detailDtos);
            var eventEntity = await eventRepository.GetByResourceIdAsync(eventResourceId).ConfigureAwait(false) ?? throw new ResourceNotFoundException($"Event with ResourceId {eventResourceId} not found.");
            eventEntity.AddUpdateDetails(mapper.Map<IEnumerable<Detail>>(detailDtos));
            await eventRepository.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task RemoveDetailAsync(Guid eventResourceId, Guid detailResourceId) {
            var eventEntity = await eventRepository.GetByResourceIdAsync(eventResourceId).ConfigureAwait(false) ?? throw new ResourceNotFoundException($"Event with ResourceId {eventResourceId} not found.");
            eventEntity.RemoveDetail(detailResourceId);
            await eventRepository.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task RemoveAllDetailsAsync(Guid eventResourceId) {
            var eventEntity = await eventRepository.GetByResourceIdAsync(eventResourceId).ConfigureAwait(false) ?? throw new ResourceNotFoundException($"Event with ResourceId {eventResourceId} not found.");
            eventEntity.RemoveAllDetails();
            await eventRepository.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
