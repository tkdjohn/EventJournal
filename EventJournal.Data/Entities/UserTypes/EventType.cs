using System.ComponentModel.DataAnnotations;

namespace EventJournal.Data.Entities.UserTypes {
    public class EventType : BaseEntity {
        [Key]
        public new int Id { get { return base.Id; } set { base.Id = value; } }

        [Required]
        public new Guid ResourceId { get { return base.ResourceId; } set { base.ResourceId = value; } }

        [Required]
        public required string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        //TODO: does this belong here??
        public static EventType CreateDefaultEventType() {
            return new EventType { ResourceId = Guid.NewGuid(), Name = "Random Event", Description = "Use for tracking random things like onset of pain, headache, or whatever that isn't directly associated with a specific even type." };
        }

        internal override void CopyUserValues<T>(T source) {
            var sourceEventType = source as EventType ?? throw new InvalidCastException($"{nameof(source)} is not of type {typeof(EventType)}");
            Name = sourceEventType.Name;
            Description = sourceEventType.Description;
        }
    }
}
