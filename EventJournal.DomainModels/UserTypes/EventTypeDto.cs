using System.ComponentModel.DataAnnotations;

namespace EventJournal.DomainDto.UserTypes {
    public class EventTypeDto : BaseDto {
        public override Guid ResourceId { get; set; }

        [Required]
        public required string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        internal override void CopyUserValues<T>(T source) {
            var sourceEventType = source as EventTypeDto ?? throw new InvalidCastException($"{nameof(source)} is not of type {typeof(EventTypeDto)}");
            Name = sourceEventType.Name;
            Description = sourceEventType.Description;
        }

        public override string ToString() {
            return this.Serialize();
        }
    }
}

