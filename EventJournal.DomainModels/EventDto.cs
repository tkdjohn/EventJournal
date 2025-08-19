using EventJournal.DomainDto.UserTypes;
using System.ComponentModel.DataAnnotations;

namespace EventJournal.DomainDto {
    public class EventDto : BaseDto {
        public Guid EventResourceId { get { return ResourceId; } set { ResourceId = value; } }

        [Required]
        public EventTypeDto EventType { get; set; } = EventTypeDto.DefaultEventTypeDto();

        [Required]
        public DateTime StartTime { get; set; } = DateTime.Now;

        public DateTime? EndTime { get; set; } = null;

        [MaxLength(500)]
        public string? Description { get; set; }

        public IEnumerable<DetailDto> Details { get; set; } = [];

        public static EventDto DefaultEventDto() {
            return new EventDto {
                EventResourceId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMinutes(1).AddSeconds(1),
                Description = "Description"
            };
        }

        internal override void CopyUserValues<T>(T source) {
            var soruceEvent = source as EventDto ?? throw new InvalidCastException($"{nameof(source)} is not of type {typeof(EventDto)}");
            EventType = soruceEvent.EventType;
            StartTime = soruceEvent.StartTime;
            EndTime = soruceEvent.EndTime;
            Description = soruceEvent.Description;
            Details = soruceEvent.Details;
        }
    }
}
