using EventJournal.DomainDto.UserTypes;
using System.ComponentModel.DataAnnotations;

namespace EventJournal.DomainDto {
    public class EventDto : BaseDto {
        public override Guid ResourceId { get; set; }

        [Required]
        public required EventTypeDto EventType { get; set; }

        [Required]
        public DateTime StartTime { get; set; } = DateTime.Now;

        public DateTime? EndTime { get; set; } = null;

        [MaxLength(500)]
        public string? Description { get; set; }

        public IEnumerable<DetailDto> Details { get; set; } = [];

        internal override void CopyUserValues<T>(T source) {
            var soruceEvent = source as EventDto ?? throw new InvalidCastException($"{nameof(source)} is not of type {typeof(EventDto)}");
            EventType = soruceEvent.EventType;
            StartTime = soruceEvent.StartTime;
            EndTime = soruceEvent.EndTime;
            Description = soruceEvent.Description;
            Details = soruceEvent.Details;
        }

        public override string ToString() {
            return this.Serialize();
        }
    }
}
