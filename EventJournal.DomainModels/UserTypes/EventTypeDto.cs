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
                CreateDefaultEventType(),
                new EventTypeDto { EventTypeResourceId = Guid.NewGuid(), Name = "Exercise" },
                new EventTypeDto { EventTypeResourceId = Guid.NewGuid(), Name = "Bathroom Visit" },
                new EventTypeDto { EventTypeResourceId = Guid.NewGuid(), Name = "Food Consumption" },
                new EventTypeDto { EventTypeResourceId = Guid.NewGuid(), Name = "WeighIn" },
            ];
        }

        //TODO:  does this belong here??
        public static EventTypeDto CreateDefaultEventType() {
            return new EventTypeDto { EventTypeResourceId = Guid.NewGuid(), Name = "Random Event", Description = "Use for tracking random things like onset of pain, headache, or whatever that isn't directly associated with a specific even type." };
        }

        internal override void CopyUserValues<T>(T source) {
            var sourceEventType = source as EventTypeDto ?? throw new InvalidCastException($"{nameof(source)} is not of type {typeof(EventTypeDto)}");
            Name = sourceEventType.Name;
            Description = sourceEventType.Description;
        }
    }

}

