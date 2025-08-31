using AutoMapper;
using EventJournal.Data;
using EventJournal.Data.Entities;
using EventJournal.DomainDto;
using EventJournal.DomainDto.UserTypes;
using EventJournal.DomainService.Exceptions;

namespace EventJournal.DomainService {
    public class EventService(
        IEventRepository eventRepository,
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
            var savedEntity = await AddUpdateEventInternalAsync(mapper.Map<Event>(dto)).ConfigureAwait(false);
            await eventRepository.SaveChangesAsync().ConfigureAwait(false);
            return mapper.Map<EventDto>(savedEntity);
        }
        private Task<Event> AddUpdateEventInternalAsync(Event entity) {
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
                Event @event = await AddUpdateEventInternalAsync(mapper.Map<Event>(dto)).ConfigureAwait(false);
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

        public async Task<Detail> AddUpdateDetailAsync(Guid eventResourceId, DetailDto detailDto) {
            ArgumentNullException.ThrowIfNull(detailDto);
            var eventEntity = await eventRepository.GetByResourceIdAsync(eventResourceId).ConfigureAwait(false) ?? throw new ResourceNotFoundException($"Event with ResourceId {eventResourceId} not found.");
            var result = eventEntity.AddUpdateDetail(mapper.Map<Detail>(detailDto));
            await eventRepository.SaveChangesAsync().ConfigureAwait(false);
            return result;
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


        public static readonly Guid DefaultEventResourceId = Guid.Parse("00000000-0000-0000-0000-000000000001");
        public static readonly Guid DefaultDetailResourceId = Guid.Parse("00000000-0000-0000-0000-000000000001");
        public Task AddResetTestDataAsync() {
            
            return AddUpdateEventAsync(new EventDto {
                ResourceId = DefaultEventResourceId,
                StartTime = DateTime.Now,
                Description = "Event History Started",
                EventType = EventTypeDto.DefaultEventTypeDtos.First(),
                Details = [ new DetailDto {
                    ResourceId = DefaultDetailResourceId,
                    DetailType = DetailTypeDto.DefaultDetailTypeDto,
                    Intensity = DetailTypeDto.DefaultDetailTypeDto.AllowedIntensities.First(),
                    Notes = "Congratulations on starting your Event History!\nTake the next step and add another event!"
                }
                    ]
            });
        }
    }
}
