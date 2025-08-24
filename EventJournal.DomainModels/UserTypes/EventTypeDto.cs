using System.ComponentModel.DataAnnotations;

namespace EventJournal.DomainDto.UserTypes {
    public class EventTypeDto : BaseDto {
        public override Guid ResourceId { get; set; }

        [Required]
        public required string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public static readonly EventTypeDto DefaultEventTypeDto = new() { 
            ResourceId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            Name = "Random Event",
            Description = "Use for tracking random things like onset of pain, headache, or whatever that isn't directly associated with a specific event type." 
        };

        public static readonly EventTypeDto[] DefaultEventTypeDtos = [
            DefaultEventTypeDto,
            new EventTypeDto { ResourceId = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Exercise" },
            new EventTypeDto { ResourceId = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "Bathroom Visit" },
            new EventTypeDto { ResourceId = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = "Food Consumption" },
            new EventTypeDto { ResourceId = Guid.Parse("00000000-0000-0000-0000-000000000005"), Name = "Weigh In" },
        ];

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

