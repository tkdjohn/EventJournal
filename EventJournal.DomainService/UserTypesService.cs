using AutoMapper;
using EventJournal.Data.Entities.UserTypes;
using EventJournal.Data.UserTypeRepositories;
using EventJournal.DomainDto.UserTypes;
using EventJournal.DomainService.Exceptions;

namespace EventJournal.DomainService {
    public class UserTypesService(
        IEventTypeRepository eventTypeRepository,
        IDetailTypeRepository detailTypeRepository,
        IMapper mapper)
    : IUserTypesService, IInternalUserTypeService {


        // ======================> Event Types <======================
        public async Task<IList<EventTypeDto>> GetAllEventTypesAsync() {
            return mapper.Map<IList<EventTypeDto>>(await eventTypeRepository.GetAllAsync().ConfigureAwait(false));
        }

        public async Task<EventTypeDto?> GetEventTypeByIdAsync(Guid resourceId) {
            return mapper.Map<EventTypeDto?>(await eventTypeRepository.GetByResourceIdAsync(resourceId).ConfigureAwait(false));
        }

        public async Task<EventTypeDto> AddUpdateEventTypeAsync(EventTypeDto dto) {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));
            //TODO: additional validations?
            var savedEntity = await AddUpdateEventTypePrivateAsync(mapper.Map<EventType>(dto)).ConfigureAwait(false);
            await eventTypeRepository.SaveChangesAsync().ConfigureAwait(false);
            return mapper.Map<EventTypeDto>(savedEntity);
        }
        private Task<EventType> AddUpdateEventTypePrivateAsync(EventType entity) {
            return eventTypeRepository.AddUpdateAsync(entity);
        }
        public async Task<IEnumerable<EventTypeDto>> AddUpdateEventTypesAsync(IEnumerable<EventTypeDto> dtos) {
            ArgumentNullException.ThrowIfNull(dtos, nameof(dtos));
            List<EventType> eventTypes = [];
            foreach (var dto in dtos) {
                EventType eventType = await AddUpdateEventTypePrivateAsync(mapper.Map<EventType>(dto)).ConfigureAwait(false);
                eventTypes.AddRange(eventType);
            }
            await eventTypeRepository.SaveChangesAsync().ConfigureAwait(false);
            return mapper.Map<List<EventTypeDto>>(eventTypes);
        }

        public async Task DeleteEventTypeAsync(Guid resourceId) {
            var entity = await eventTypeRepository.GetByResourceIdAsync(resourceId).ConfigureAwait(false);
            if (entity == null) {
                return;
            }
            eventTypeRepository.Delete(entity);
            await eventTypeRepository.SaveChangesAsync().ConfigureAwait(false);
        }

        public Task<EventType?> GetEventTypeEntityAsync(Guid resourceId) {
            return eventTypeRepository.GetByResourceIdAsync(resourceId);
        }
        // ======================> Detail Types <======================
        public async Task<IList<DetailTypeDto>> GetAllDetailTypesAsync() {
            return mapper.Map<IList<DetailTypeDto>>(await detailTypeRepository.GetAllAsync().ConfigureAwait(false));
        }

        public async Task<DetailTypeDto?> GetDetailTypeByIdAsync(Guid resourceId) {
            return mapper.Map<DetailTypeDto?>(await detailTypeRepository.GetByResourceIdAsync(resourceId).ConfigureAwait(false));
        }

        public async Task<DetailTypeDto> AddUpdateDetailTypeAsync(DetailTypeDto dto) {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));
            //TODO: additional validations?
            var entity = await AddUpdateDetailTypePrivateAsync(mapper.Map<DetailType>(dto)).ConfigureAwait(false);
            await detailTypeRepository.SaveChangesAsync().ConfigureAwait(false);
            return mapper.Map<DetailTypeDto>(entity);
        }
        private Task<DetailType> AddUpdateDetailTypePrivateAsync(DetailType entity) {
            // don't duplicate intensities - match on either Id or ResourceId
            foreach (var intensity in entity.AllowedIntensities) {
                intensity.DetailType = entity;
                var existingIntensity = entity.AllowedIntensities.First(i => i.ResourceId == intensity.ResourceId || (i.Id != 0 && i.Id == intensity.Id));
                if (existingIntensity != null) {
                    intensity.Id = existingIntensity.Id;
                    intensity.ResourceId = existingIntensity.ResourceId;
                }
            }
            return detailTypeRepository.AddUpdateAsync(entity);
        }
        public async Task<IEnumerable<DetailTypeDto>> AddUpdateDetailTypesAsync(IEnumerable<DetailTypeDto> dtos) {
            ArgumentNullException.ThrowIfNull(dtos, nameof(dtos));
            List<DetailType> detailTypeEntities = [];
            foreach (var dto in dtos) {
                detailTypeEntities.Add( await AddUpdateDetailTypePrivateAsync(mapper.Map<DetailType>(dto)).ConfigureAwait(false));
            }
            await detailTypeRepository.SaveChangesAsync().ConfigureAwait(false);
            return mapper.Map<List<DetailTypeDto>>(detailTypeEntities);
        }

        public async Task DeleteDetailTypeAsync(Guid resourceId) {
            var entity = await detailTypeRepository.GetByResourceIdAsync(resourceId).ConfigureAwait(false);
            if (entity == null) {
                return;
            }
            detailTypeRepository.Delete(entity);
            await detailTypeRepository.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<IntensityDto> AddUpdateAllowedIntensityAsync(Guid detailTypeResourceId, IntensityDto intensityDto) {
            ArgumentNullException.ThrowIfNull(intensityDto, nameof(intensityDto));
            var result = AddUpdateAllowedIntensityPrivateAsync(detailTypeResourceId, mapper.Map<Intensity>(intensityDto));
            await detailTypeRepository.SaveChangesAsync().ConfigureAwait(false);
            return mapper.Map<IntensityDto>(result);
        }
        private async Task<Intensity> AddUpdateAllowedIntensityPrivateAsync(Guid detailTypeResourceId, Intensity intensity) {
            var detailTypeEntity = await detailTypeRepository.GetByResourceIdAsync(detailTypeResourceId).ConfigureAwait(false) ?? throw new ResourceNotFoundException($"DetailType with ResourceId {detailTypeResourceId} not found.");
            var result = detailTypeEntity.AddUpdateAllowedIntensity(intensity);
            await detailTypeRepository.SaveChangesAsync().ConfigureAwait(false);
            return result;
        }
        public async Task AddUpdateAllowedIntensitiesAsync(Guid detailTypeResourceId, IEnumerable<IntensityDto> intensityDtos) {
            foreach (var intensityDto in intensityDtos) {
                await AddUpdateAllowedIntensityPrivateAsync(detailTypeResourceId, mapper.Map<Intensity>(intensityDto)).ConfigureAwait(false);
            }
            await detailTypeRepository.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task RemoveAllowedIntensityAsync(Guid detailTypeResourceId, Guid intensityResourceId) {
            var detailTypeEntity = await detailTypeRepository.GetByResourceIdAsync(detailTypeResourceId).ConfigureAwait(false) ?? throw new ResourceNotFoundException($"DetailType with ResourceId {detailTypeResourceId} not found.");
            detailTypeEntity.RemoveIntensity(intensityResourceId);
            await detailTypeRepository.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task RemoveAllAllowedIntensitiesAsync(Guid detailTypeResourceId) {
            var detailTypeEntity = await detailTypeRepository.GetByResourceIdAsync(detailTypeResourceId).ConfigureAwait(false) ?? throw new ResourceNotFoundException($"DetailType with ResourceId {detailTypeResourceId} not found.");
            detailTypeEntity.RemoveAllIntensities();
            await detailTypeRepository.SaveChangesAsync().ConfigureAwait(false);
        }

        public Task<DetailType?> GetDetailTypeEntityAsync(Guid resourceId) {
            return detailTypeRepository.GetByResourceIdAsync(resourceId);
        }
    }
}
