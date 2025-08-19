using System.ComponentModel.DataAnnotations;

namespace EventJournal.DomainDto.UserTypes {
    public class EventTypeDto : BaseDto {
        public Guid EventTypeResourceId { get { return ResourceId; } set { ResourceId = value; } }

        [Required]
        public required string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        //TODO: does this belong here??
        public static IEnumerable<EventTypeDto> DefaultEventTypes() {
            return [
                DefaultEventTypeDto(),
                new EventTypeDto { EventTypeResourceId = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Exercise" },
                new EventTypeDto { EventTypeResourceId = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Bathroom Visit" },
                new EventTypeDto { EventTypeResourceId = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "Food Consumption" },
                new EventTypeDto { EventTypeResourceId = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = "WeighIn" },
            ];
        }

        //TODO: does this belong here?? maybe should ensure default event type is created in the database at startup, and then use that instead of creating a new one here.
        public static EventTypeDto DefaultEventTypeDto() {
            return new EventTypeDto { 
                EventTypeResourceId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                Name = "Random Event",
                Description = "Use for tracking random things like onset of pain, headache, or whatever that isn't directly associated with a specific even type." 
            };
        }

        internal override void CopyUserValues<T>(T source) {
            var sourceEventType = source as EventTypeDto ?? throw new InvalidCastException($"{nameof(source)} is not of type {typeof(EventTypeDto)}");
            Name = sourceEventType.Name;
            Description = sourceEventType.Description;
        }
    }
}

