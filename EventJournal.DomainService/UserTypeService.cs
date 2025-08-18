using AutoMapper;
using EventJournal.Data.Entities.UserTypes;
using EventJournal.Data.UserTypeRepositories;
using EventJournal.DomainDto.UserTypes;
using EventJournal.DomainService.Exceptions;

namespace EventJournal.DomainService {
    public class UserTypeService(
        IDetailTypeRepository detailRepository,
        IEventTypeRepository eventTypeRepository,
        IIntensityRepository intensityRepository,
        Mapper mapper) : IUserTypeService {

        public async Task<IEnumerable<DetailTypeDto>> GetAllDetailTypesAsync() {
            return mapper.Map<IEnumerable<DetailTypeDto>>(await detailRepository.GetAllAsync().ConfigureAwait(false));
        }
        public async Task<DetailTypeDto?> GetDetailTypeByIdAsync(Guid resourceId) {
            return mapper.Map<DetailTypeDto?>(await detailRepository.GetByResourceIdAsync(resourceId).ConfigureAwait(false));
        }
        public async Task<DetailTypeDto> AddUpdateDetailTypeAsync(DetailTypeDto dto) {
            ArgumentNullException.ThrowIfNull(dto);
            //TODO: additional validations?
            return mapper.Map<DetailTypeDto>(await detailRepository.AddUpdateAsync(mapper.Map<DetailType>(dto)).ConfigureAwait(false));
        }
        public async Task DeleteDetailTypeAsync(Guid resourceId) {
            var entity = await detailRepository.GetByResourceIdAsync(resourceId).ConfigureAwait(false);
            ResourceNotFoundException.ThrowIfNull(entity, $"Event with resource id {resourceId} not found");
            await detailRepository.DeleteAsync(entity);
        }

        public async Task<IEnumerable<EventTypeDto>> GetAllEventTypesAsync() {
            return mapper.Map<IEnumerable<EventTypeDto>>(await eventTypeRepository.GetAllAsync().ConfigureAwait(false));
        }
        public async Task<EventTypeDto?> GetEventTypeByIdAsync(Guid resourceId) {
            return mapper.Map<EventTypeDto?>(await eventTypeRepository.GetByResourceIdAsync(resourceId).ConfigureAwait(false));
        }
        public async Task<EventTypeDto> AddUpdateEventTypeAsync(EventTypeDto dto) {
            ArgumentNullException.ThrowIfNull(dto);
            //TODO: additional validations?
            return mapper.Map<EventTypeDto>(await eventTypeRepository.AddUpdateAsync(mapper.Map<EventType>(dto)).ConfigureAwait(false));
        }
        public async Task DeleteEventTypeAsync(Guid resourceId) {
            var entity = await eventTypeRepository.GetByResourceIdAsync(resourceId).ConfigureAwait(false);
            ResourceNotFoundException.ThrowIfNull(entity, $"Event with resource id {resourceId} not found");
            await eventTypeRepository.DeleteAsync(entity);
        }

        public async Task<IEnumerable<IntensityDto>> GetAllIntensitiesAsync() {
            return mapper.Map<IEnumerable<IntensityDto>>(await intensityRepository.GetAllAsync().ConfigureAwait(false));
        }
        public async Task<IntensityDto?> GetIntensityByIdAsync(Guid resourceId) {
            return mapper.Map<IntensityDto?>(await intensityRepository.GetByResourceIdAsync(resourceId).ConfigureAwait(false));
        }
        public async Task<IntensityDto> AddUpdateIntensityAsync(IntensityDto dto) {
            ArgumentNullException.ThrowIfNull(dto);
            //TODO: additional validations?
            return mapper.Map<IntensityDto>(await intensityRepository.AddUpdateAsync(mapper.Map<Intensity>(dto)).ConfigureAwait(false));
        }
        public async Task DeleteIntensityAsync(Guid resourceId) {
            var entity = await intensityRepository.GetByResourceIdAsync(resourceId).ConfigureAwait(false);
            ResourceNotFoundException.ThrowIfNull(entity, $"Event with resource id {resourceId} not found");
            await intensityRepository.DeleteAsync(entity);
        }
    }
}
