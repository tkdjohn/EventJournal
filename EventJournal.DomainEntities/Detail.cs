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
            var sourceDetail = source as Detail ?? throw new InvalidCastException($"{nameof(source)} is not of type {typeof(Detail)}");
            Event = sourceDetail.Event;
            DetailType = sourceDetail.DetailType;
            Intensity = sourceDetail.Intensity;
            Notes = sourceDetail.Notes;
        }
    }
}
