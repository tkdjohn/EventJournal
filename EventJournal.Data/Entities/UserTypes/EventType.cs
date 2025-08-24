using System.ComponentModel.DataAnnotations;

namespace EventJournal.Data.Entities.UserTypes {
    public class EventType : BaseEntity {
        [Key]
        public override int Id { get; set; }

        [Required]
        public override Guid ResourceId { get; set; }

        [Required]
        public required string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        internal override void CopyUserValues<T>(T source) {
            var sourceEventType = source as EventType ?? throw new InvalidCastException($"{nameof(source)} is not of type {typeof(EventType)}");
            Name = sourceEventType.Name;
            Description = sourceEventType.Description;
        }
    }
}
