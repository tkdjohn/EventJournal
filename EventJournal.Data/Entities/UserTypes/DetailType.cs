using System.ComponentModel.DataAnnotations;

namespace EventJournal.Data.Entities.UserTypes {
    public class DetailType : BaseEntity {
        [Key]
        public int DetailTypeId { get { return Id; } set { Id = value; } }

        [Required]
        public Guid DetailTypeResourceId { get { return ResourceId; } set { ResourceId = value; } }

        [Required]
        public required string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public IEnumerable<Intensity> AllowedIntensities { get; set; } = [];

        internal override void CopyUserValues<T>(T source) {
            var sourceDetailType = source as DetailType ?? throw new InvalidCastException($"{nameof(source)} is not of type {typeof(DetailType)}");
            Name = sourceDetailType.Name;
            Description = sourceDetailType.Description;
        }
    }
}
