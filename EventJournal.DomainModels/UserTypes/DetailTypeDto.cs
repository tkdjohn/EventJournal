using EventJournal.Common.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace EventJournal.DomainDto.UserTypes {
    public class DetailTypeDto : BaseDto {
        public override Guid ResourceId { get; set; }

        [Required]
        public required string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        //TODO: is this useful/needed?
        public IEnumerable<IntensityDto> AllowedIntensities { get; set; } = [];

        [Required]
        public required SortType IntensitySortType { get; set; } = SortType.Descending;

        internal override void CopyUserValues<T>(T source) {
            var sourceDetailType = source as DetailTypeDto ?? throw new InvalidCastException($"{nameof(source)} is not of type {typeof(DetailTypeDto)}");
            Name = sourceDetailType.Name;
            Description = sourceDetailType.Description;
            IntensitySortType = sourceDetailType.IntensitySortType;
            //TODO: this might need to be a deep copy depending on usage
            // or might need to copy by value instead of reference (eg. call inteisity.copyvaules for each item)
            AllowedIntensities = sourceDetailType.AllowedIntensities;
        }

        public override string ToString() {
            return this.Serialize();
        }
    }
}
