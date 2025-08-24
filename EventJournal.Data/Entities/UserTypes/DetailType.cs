using EventJournal.Data.Entities.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace EventJournal.Data.Entities.UserTypes {
    public class DetailType : BaseEntity {
        [Key]
        public override int Id { get; set; }

        [Required]
        public override Guid ResourceId { get; set; }

        [Required]
        public required string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public SortType IntensitySortType { get; set; } = SortType.None;

        public ICollection<Intensity> AllowedIntensities { get; set; } = [];

        internal override void CopyUserValues<T>(T source) {
            var sourceDetailType = source as DetailType ?? throw new InvalidCastException($"{nameof(source)} is not of type {typeof(DetailType)}");
            Name = sourceDetailType.Name;
            Description = sourceDetailType.Description;
        }
    }
}
