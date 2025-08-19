using EventJournal.DomainDto.UserTypes;
using System.ComponentModel.DataAnnotations;

namespace EventJournal.DomainDto {
    public class DetailDto : BaseDto {
        public Guid DetailResourceId { get { return ResourceId; } set { ResourceId = value; } }

        [Required]
        public virtual required DetailTypeDto DetailType { get; set; }

        [Required]
        public virtual required IntensityDto Intensity { get; set; }

        [MaxLength(512)]
        public string? Notes { get; set; }

        public static DetailDto DefaultDetailDto() {
            var detailType = DetailTypeDto.DefaultDetailTypeDto();
            return new DetailDto { 
                DetailResourceId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                DetailType = detailType,
                Intensity = IntensityDto.DefaultIntensityDto(detailType.ResourceId),
                Notes = "Notes\nmore notes"
            };
        }
 
        internal override void CopyUserValues<T>(T source) {
            var sourceDetail = source as DetailDto ?? throw new InvalidCastException($"{nameof(source)} is not of type {typeof(DetailDto)}");
            DetailType = sourceDetail.DetailType;
            Intensity = sourceDetail.Intensity;
            Notes = sourceDetail.Notes;
        }
    }
}
