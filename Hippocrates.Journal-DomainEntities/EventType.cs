using System.ComponentModel.DataAnnotations;

namespace Hippocrates.Journal.DomainEntities {
    public class EventType : BaseEntity {
        [Key] public int EventTypeId { get { return Id; } set { Id = value; } }
        [Required] public Guid EventTypeResourceId { get { return ResourceId; } set { ResourceId = value; } }
        [Required] public required string Name { get; set; }
        [MaxLength(500)] public string? Description { get; set; }

        //TODO: this code really belongs in a service or maybe repo
        public static IEnumerable<EventType> DefaultEventTypes() {
            return [
                new EventType { EventTypeResourceId = Guid.NewGuid(), Name = "Random Event", Description = "Use for tracking random things like onset of pain, headache, or whatever that isn't directly associated with a specific even type."},
                new EventType { EventTypeResourceId = Guid.NewGuid(), Name = "Exercise" },
                new EventType { EventTypeResourceId = Guid.NewGuid(), Name = "Bathroom visit" },
                new EventType { EventTypeResourceId = Guid.NewGuid(), Name = "Meal" },
            ];
        }
    }
}
