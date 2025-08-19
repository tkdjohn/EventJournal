using EventJournal.DomainDto.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace EventJournal.DomainDto.UserTypes {
    public class IntensityDto : BaseDto {
        public Guid IntensityResourceId { get { return ResourceId; } set { ResourceId = value; } }

        [Required, MaxLength(50)]
        public required string Name { get; set; }

        [Required]
        public required int Level { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public required SortType DefaultSortType { get; set; }


        [Required]
        public required Guid DetailTypeId { get; set; }
        //TODO:  does this belong here??
        public static IntensityDto DefaultIntensityDto(Guid detailTypeId) {
            return new IntensityDto {
                IntensityResourceId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                DefaultSortType = SortType.Descending,
                Level = 1,
                Name = "Default Intensity DTO",
                Description = "Description",
                DetailTypeId = detailTypeId
            };
        }

        internal override void CopyUserValues<T>(T source) {
            var sourceIntensity = source as IntensityDto ?? throw new InvalidCastException($"{nameof(source)} is not of type {typeof(IntensityDto)}");
            Name = sourceIntensity.Name;
            Level = sourceIntensity.Level;
            Description = sourceIntensity.Description;
            DefaultSortType = sourceIntensity.DefaultSortType;
        }
    }
}

