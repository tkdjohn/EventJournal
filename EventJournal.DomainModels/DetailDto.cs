using EventJournal.DomainDto.UserTypes;
using System.ComponentModel.DataAnnotations;

namespace EventJournal.DomainDto {
    public class DetailDto : BaseDto {
        public override Guid ResourceId { get; set; }

        [Required]
        public required EventDto EventDto { get; set; }

        [Required]
        public virtual required DetailTypeDto DetailType { get; set; }

        [Required]
        public virtual required IntensityDto Intensity { get; set; }

        [MaxLength(512)]
        public string? Notes { get; set; }

        public static readonly DetailDto DefaultDetailDto = new() {
            ResourceId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            EventDto = EventDto.DefaultEventDto,
            DetailType = DetailTypeDto.DefaultDetailTypeDto,
            Intensity = DetailTypeDto.DefaultDetailTypeDto.AllowedIntensities.First(),
            Notes = "Congratulations on starting your Event History!\nTake the next step and add another event!"
        };
 
        internal override void CopyUserValues<T>(T source) {
            var sourceDetail = source as DetailDto ?? throw new InvalidCastException($"{nameof(source)} is not of type {typeof(DetailDto)}");
            DetailType = sourceDetail.DetailType;
            Intensity = sourceDetail.Intensity;
            Notes = sourceDetail.Notes;
        }
        public override string ToString() {
            return this.Serialize();
        }
    }
}
