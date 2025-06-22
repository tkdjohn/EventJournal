using EventJournal.DomainEntities.UserTypes;
using System.ComponentModel.DataAnnotations;

namespace EventJournal.DomainEntities {
    public class Detail : BaseEntity{
        [Key] public int DetailId { get { return Id; } set { Id = value; } }
        [Required] public Guid DetailResourceId { get { return ResourceId; } set { ResourceId = value; } }
        [Required] public virtual required Event Event { get; set; }
        [Required] public virtual required DetailType DetailType { get; set; }
        [Required] public virtual required Intensity Intensity { get; set; }
        [MaxLength(512)] public string? Notes { get; set; }

        internal override void CopyUserValues<T>(T source) {
            throw new NotImplementedException();
        }
    }
}
