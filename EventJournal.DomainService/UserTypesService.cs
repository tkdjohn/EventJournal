using AutoMapper;
using EventJournal.Data;
using EventJournal.Data.Entities;
using EventJournal.Data.Entities.UserTypes;
using EventJournal.Data.UserTypeRepositories;
using EventJournal.DomainDto;
using EventJournal.DomainDto.UserTypes;

namespace EventJournal.DomainService {
    public class UserTypesService(
        IEventTypeRepository eventTypeRepository, 
        IDetailTypeRepository detailTypeRepository,
        IIntensityRepository intensityRepository,
        IMapper mapper)
    : IUserTypesService {


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

        // ======================> Detail Types <======================
        public async Task<IList<DetailTypeDto>> GetAllDetailTypesAsync() {
            return mapper.Map<IList<DetailTypeDto>>(await detailTypeRepository.GetAllAsync().ConfigureAwait(false));
        }

        public async Task<DetailTypeDto?> GetDetailTypeByIdAsync(Guid resourceId) {
            return mapper.Map<DetailTypeDto?>(await detailTypeRepository.GetByResourceIdAsync(resourceId).ConfigureAwait(false));
        }

        public async Task<DetailTypeDto> AddUpdateDetailTypeAsync(DetailTypeDto dto) {
            ArgumentNullException.ThrowIfNull(dto);
            //TODO: additional validations?
            var entity = await detailTypeRepository.AddUpdateAsync(mapper.Map<DetailType>(dto)).ConfigureAwait(false);
            await detailTypeRepository.SaveChangesAsync().ConfigureAwait(false);
            return mapper.Map<DetailTypeDto>(entity);
        }

        public async Task<IEnumerable<DetailTypeDto>> AddUpdateDetailTypesAsync(IEnumerable<DetailTypeDto> dtos) {
            ArgumentNullException.ThrowIfNull(dtos);
            var detailTypeEntities = await detailTypeRepository.AddUpdateAsync(mapper.Map<IEnumerable<DetailType>>(dtos)).ConfigureAwait(false);
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

        // ======================> Intensities <======================
        public async Task<IList<IntensityDto>> GetAllIntensitiesAsync() {
            return mapper.Map<IList<IntensityDto>>(await intensityRepository.GetAllAsync().ConfigureAwait(false));
        }

        public async Task<IntensityDto?> GetIntensityByIdAsync(Guid resourceId) {
            return mapper.Map<IntensityDto?>(await intensityRepository.GetByResourceIdAsync(resourceId).ConfigureAwait(false));
        }

        public async Task<IntensityDto> AddUpdateIntensityAsync(IntensityDto dto) {
            ArgumentNullException.ThrowIfNull(dto);
            //TODO: additional validations?
            var entity = await intensityRepository.AddUpdateAsync(mapper.Map<Intensity>(dto)).ConfigureAwait(false);
            await intensityRepository.SaveChangesAsync().ConfigureAwait(false);
            return mapper.Map<IntensityDto>(entity);
        }

        public async Task<IEnumerable<IntensityDto>> AddUpdateIntensitiesAsync(IEnumerable<IntensityDto> dtos) {
            ArgumentNullException.ThrowIfNull(dtos);
            var intensityEntities = await intensityRepository.AddUpdateAsync(mapper.Map<IEnumerable<Intensity>>(dtos)).ConfigureAwait(false);
            await intensityRepository.SaveChangesAsync().ConfigureAwait(false);
            return mapper.Map<List<IntensityDto>>(intensityEntities);
        }

        public async Task DeleteIntensityAsync(Guid resourceId) {
            var entity = await intensityRepository.GetByResourceIdAsync(resourceId).ConfigureAwait(false);
            if (entity == null) {
                return;
            }
            intensityRepository.Delete(entity);
            await intensityRepository.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task AddTestDataAsync() {
            await AddUpdateEventTypesAsync(EventTypeDto.DefaultEventTypeDtos).ConfigureAwait(false);
            await AddUpdateDetailTypeAsync(DetailTypeDto.DefaultDetailTypeDto).ConfigureAwait(false);
            await AddUpdateIntensitiesAsync(IntensityDto.DefaultIntensityDtos).ConfigureAwait(false);
        }
    }
}
