using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hippocrates.Journal.DomainEntities {
    public class EventType : BaseEntity {
        [Key] public int EventTypeId { get; set; }
        [Required] public Guid EventTypeGuid { get; set; }
        [Required] public required string Name { get; set; }
        [MaxLength(500)] public string? Description { get; set; }

        public static IEnumerable<EventType> DefaultEventTypes() {
            return [
                new EventType { EventTypeGuid = Guid.NewGuid(), Name = "Random Event", Description = "Use for tracking random things like onset of pain, headache, or whatever that isn't directly associated with a specific even type."},
                new EventType { EventTypeGuid = Guid.NewGuid(), Name = "Exercise" },
                new EventType { EventTypeGuid = Guid.NewGuid(), Name = "Bathroom visit" },
                new EventType { EventTypeGuid = Guid.NewGuid(), Name = "Meal" },
            ];
        }
    }
}
