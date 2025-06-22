using System.ComponentModel.DataAnnotations;

namespace EventJournal.DomainEntities.UserTypes {
    public class DetailType : BaseEntity {
        [Key] public int DetailTypeId { get { return Id; } set { Id = value; } }
        [Required] public Guid DetailTypeResourceId { get { return ResourceId; } set { ResourceId = value; } }
        [Required] public required string Name { get; set; }
        [MaxLength(500)] public string? Description { get; set; }

        public IEnumerable<Intensity> Intensities { get; set; } = [];

        internal override void CopyUserValues<T>(T source) {
            throw new NotImplementedException();
        }
    }
}
