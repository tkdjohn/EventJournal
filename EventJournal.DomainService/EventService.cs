using AutoMapper;
using EventJournal.Data;
using EventJournal.Data.Entities;
using EventJournal.Data.Entities.UserTypes;
using EventJournal.DomainDto;
using EventJournal.DomainService.Exceptions;

namespace EventJournal.DomainService {
    public class EventService(
        IEventRepository eventRepository,
        //TODO: fix this services shouldn't call services
        IUserTypesService userTypesService,
        IMapper mapper)
    : IEventService {
        private readonly IInternalUserTypeService internalUserTypeService = userTypesService as IInternalUserTypeService ?? throw new InvalidOperationException("userTypesService must implement IInternalUserTypeService");
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
            var eventTypeEntity = await internalUserTypeService.GetEventTypeEntityAsync(dto.EventType.ResourceId).ConfigureAwait(false) ?? throw new ResourceNotFoundException($"EventType with ResourceId {dto.EventType.ResourceId} not found.");

            var savedEvent = await AddUpdateEventPrivateAsync(mapper.Map<Event>(dto), eventTypeEntity);
           
            await eventRepository.SaveChangesAsync().ConfigureAwait(false);
            return mapper.Map<EventDto>(savedEvent);
        }

        private Task<Event> AddUpdateEventPrivateAsync(Event @event, EventType eventType) {
            ArgumentNullException.ThrowIfNull(eventType);
            // don't duplicate details - match on either Id or ResourceId
            @event.EventType = eventType;
            foreach (var detail in @event.Details) {
                detail.Event = @event;
                var existingDetail = @event.Details.First(d =>  d.ResourceId == detail.ResourceId || (d.Id != 0 && d.Id == detail.Id));
                if (existingDetail != null) {
                    detail.Id = existingDetail.Id;
                    detail.ResourceId = existingDetail.ResourceId;
                }
            }

            return eventRepository.AddUpdateAsync(@event);
        }
        public async Task<IEnumerable<EventDto>> AddUpdateEventsAsync(IEnumerable<EventDto> dtos) {
            ArgumentNullException.ThrowIfNull(dtos);
            var events = new List<Event>();
            foreach (var dto in dtos) {
                //TODO: additional validations?
                var eventTypeEntity = await internalUserTypeService.GetEventTypeEntityAsync(dto.EventType.ResourceId).ConfigureAwait(false) ?? throw new ResourceNotFoundException($"EventType with ResourceId {dto.EventType.ResourceId} not found.");
                Event @event = await AddUpdateEventPrivateAsync(mapper.Map<Event>(dto), eventTypeEntity).ConfigureAwait(false);
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

            // so the problem is that we need to find existing DetailType with it's existing AllowedIntensities and THEN assign detail to detail
            // and pick the right intensity. Not sure this should happen here maybe in AddUpdateDetailAsync() ??
            // - in other wrds extend the logic in AddUpdteEventPrivateAsync to utilize similar logic  that exists in the USErTypesServie for DetailType and Intensities.
            // /// remember the goal is that Detail selects a valid intensity from the list of allowed intensities specified by detail type.
            // another stray thought: maybe we need a mapping table that maps Detail id and Intensity id?
            ArgumentNullException.ThrowIfNull(detailDto);
            var eventEntity = await eventRepository.GetByResourceIdAsync(eventResourceId).ConfigureAwait(false) ?? throw new ResourceNotFoundException($"Event with ResourceId {eventResourceId} not found.");
            
            var detailTypeEntity = await internalUserTypeService.GetDetailTypeEntityAsync(detailDto.DetailType.ResourceId).ConfigureAwait(false) ?? throw new ResourceNotFoundException($"DetailType with ResourceId {detailDto.DetailType.ResourceId} not found.");
            var intensityEntity = detailTypeEntity.AllowedIntensities.FirstOrDefault(i => i.ResourceId == detailDto.Intensity.ResourceId) ?? throw new ResourceNotFoundException($"Intensity with ResourceId {detailDto.Intensity.ResourceId} not found in DetailType with ResourceId {detailDto.DetailType.ResourceId}.");
            
            var detailEntity = mapper.Map<Detail>(detailDto);
            //TODO: move these to mapper when you replace AutoMapper
            detailEntity.DetailType = detailTypeEntity;
            detailEntity.Intensity = intensityEntity;

            var result = eventEntity.AddUpdateDetail(detailEntity);
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
