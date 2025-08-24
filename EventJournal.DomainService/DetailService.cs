using AutoMapper;
using EventJournal.Data;
using EventJournal.Data.Entities;
using EventJournal.Data.Entities.UserTypes;
using EventJournal.Data.UserTypeRepositories;
using EventJournal.DomainDto;
using EventJournal.DomainDto.UserTypes;

namespace EventJournal.DomainService {
    public class DetailService(
        IDetailRepository detailRepository,
        IDetailTypeRepository detailTypeRepository,
        IIntensityRepository intensityRepository,
        IMapper mapper)
    : IDetailService {


        // ======================> Details <======================
        public async Task<IList<DetailDto>> GetAllDetailsAsync() {
            return mapper.Map<IList<DetailDto>>(await detailRepository.GetAllAsync().ConfigureAwait(false));
        }

        public async Task<DetailDto?> GetDetailByIdAsync(Guid resourceId) {
            return mapper.Map<DetailDto?>(await detailRepository.GetByResourceIdAsync(resourceId).ConfigureAwait(false));
        }

        public async Task<DetailDto> AddUpdateDetailAsync(DetailDto dto) {
            ArgumentNullException.ThrowIfNull(dto);
            //TODO: additional validations?
            var entity = await detailRepository.AddUpdateAsync(mapper.Map<Detail>(dto)).ConfigureAwait(false);
            await detailRepository.SaveChangesAsync().ConfigureAwait(false);
            return mapper.Map<DetailDto>(entity);
        }

        public async Task<IEnumerable<DetailDto>> AddUpdateDetailsAsync(IEnumerable<DetailDto> dtos) {
            ArgumentNullException.ThrowIfNull(dtos);
            var detailEntities = await detailRepository.AddUpdateAsync(mapper.Map<IEnumerable<Detail>>(dtos)).ConfigureAwait(false);
            await detailRepository.SaveChangesAsync().ConfigureAwait(false);
            return mapper.Map<List<DetailDto>>(detailEntities);
        }

        public async Task DeleteDetailAsync(Guid resourceId) {
            var entity = await detailRepository.GetByResourceIdAsync(resourceId).ConfigureAwait(false);
            if (entity == null) {
                return;
            }
            detailRepository.Delete(entity);
            await detailRepository.SaveChangesAsync().ConfigureAwait(false);
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
            await AddUpdateDetailTypeAsync(DetailTypeDto.DefaultDetailTypeDto).ConfigureAwait(false);
            await AddUpdateIntensitiesAsync(IntensityDto.DefaultIntensityDtos).ConfigureAwait(false);
            //await AddUpdateDetailAsync(DetailDto.DefaultDetailDto).ConfigureAwait(false);
        }
    }
}
