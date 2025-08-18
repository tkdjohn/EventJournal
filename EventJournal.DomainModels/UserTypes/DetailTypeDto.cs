using System.ComponentModel.DataAnnotations;

namespace EventJournal.DomainDto.UserTypes {
    public class DetailTypeDto : BaseDto {
        public Guid DetailTypeResourceId { get { return ResourceId; } set { ResourceId = value; } }

        [Required]
        public required string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public IEnumerable<IntensityDto> Intensities { get; set; } = [];

        internal override void CopyUserValues<T>(T source) {
            var sourceDetailType = source as DetailTypeDto ?? throw new InvalidCastException($"{nameof(source)} is not of type {typeof(DetailTypeDto)}");
            Name = sourceDetailType.Name;
            Description = sourceDetailType.Description;
        }
    }
}
