using System.ComponentModel.DataAnnotations;

namespace EventJournal.DomainDto.UserTypes {
    public class IntensityDto : BaseDto {
        public override Guid ResourceId { get; set; }

        [Required, MaxLength(50)]
        public required string Name { get; set; }

        [Required]
        public required int Level { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        internal override void CopyUserValues<T>(T source) {
            var sourceIntensity = source as IntensityDto ?? throw new InvalidCastException($"{nameof(source)} is not of type {typeof(IntensityDto)}");
            Name = sourceIntensity.Name;
            Level = sourceIntensity.Level;
            Description = sourceIntensity.Description;
        }
        public override string ToString() {
            return this.Serialize();
        }
    }
}

