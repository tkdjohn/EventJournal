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
                var existingDetail = @event.Details.First(d => d.ResourceId == detail.ResourceId || (d.Id != 0 && d.Id == detail.Id));
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
            ArgumentNullException.ThrowIfNull(detailDto);

            var result = AddUpdateDetailPrivateAsync(eventResourceId, mapper.Map<Detail>(detailDto));
            await eventRepository.SaveChangesAsync().ConfigureAwait(false);
            return mapper.Map<DetailDto>(result);
        }
        private async Task<Detail> AddUpdateDetailPrivateAsync(Guid eventResourceId, Detail detail) {
            ArgumentNullException.ThrowIfNull(detail);
            var eventEntity = await eventRepository.GetByResourceIdAsync(eventResourceId).ConfigureAwait(false) ?? throw new ResourceNotFoundException($"Event with ResourceId {eventResourceId} not found.");
            var detailTypeEntity = await internalUserTypeService.GetDetailTypeEntityAsync(detail.DetailType.ResourceId).ConfigureAwait(false) ?? throw new ResourceNotFoundException($"DetailType with ResourceId {detail.DetailType.ResourceId} not found.");
            var intensityEntity = detailTypeEntity.AllowedIntensities.FirstOrDefault(i => i.ResourceId == detail.Intensity.ResourceId) ?? throw new ResourceNotFoundException($"Intensity with ResourceId {detail.Intensity.ResourceId} not found in DetailType with ResourceId {detail.DetailType.ResourceId}.");
            detail.DetailType = detailTypeEntity;
            detail.Intensity = intensityEntity;
            return eventEntity.AddUpdateDetail(detail);
        }
        public async Task AddUpdateDetailsAsync(Guid eventResourceId, IEnumerable<DetailDto> detailDtos) {
            foreach (var detailDto in detailDtos) {
                await AddUpdateDetailPrivateAsync(eventResourceId, mapper.Map<Detail>(detailDto)).ConfigureAwait(false);
            }
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
