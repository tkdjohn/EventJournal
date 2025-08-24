using EventJournal.DomainDto.Enumerations;
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

        public required DetailTypeDto DetailTypeDto { get; set; }

        public static readonly IntensityDto ZeroIntensityDto = new() {
            ResourceId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            Level = 0,
            Name = "Zero Intensity",
            Description = "Not intense at all. Normal.",
            DetailTypeDto = DetailTypeDto.DefaultDetailTypeDto
        };

        public static readonly IntensityDto MildIntensityDto = new() {
            ResourceId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
            Level = 1,
            Name = "Mild Intensity",
            Description = "Slightly intense, but nothing special.",
            DetailTypeDto = DetailTypeDto.DefaultDetailTypeDto
        };

        public static readonly IntensityDto ModerateIntensityDto = new() {
            ResourceId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
            Level = 2,
            Name = "Moderate Intensity",
            Description = "Intense but not very unbearable or uncomfortable.",
            DetailTypeDto = DetailTypeDto.DefaultDetailTypeDto
        };

        public static readonly IntensityDto HighIntensityDto = new() {
            ResourceId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
            Level = 3,
            Name = "High Intensity",
            Description = "Intensity that is uncomfortable but still bearable.",
            DetailTypeDto = DetailTypeDto.DefaultDetailTypeDto
        };

        public static readonly IntensityDto InsaneIntensityDto = new() {
            ResourceId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
            Level = 4,
            Name = "Insane Intensity",
            Description = "Unbearable. Insane. No one needs to experience this.",
            DetailTypeDto = DetailTypeDto.DefaultDetailTypeDto
        };

        public static readonly IntensityDto[] DefaultIntensityDtos = [
            ZeroIntensityDto,
            MildIntensityDto,
            ModerateIntensityDto,
            HighIntensityDto,
            InsaneIntensityDto
        ];

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

