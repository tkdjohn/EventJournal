using System.ComponentModel.DataAnnotations;

namespace Hippocrates.Journal.DomainEntities {
    public class Event : BaseEntity {
        [Key] public int EventId { get { return Id; } set { Id = value; } }
        [Required] public Guid EventResourceId { get { return ResourceId; } set { ResourceId = value; } }
        [Required] public DateTime Timestamp { get; set; } = DateTime.Now;
        [MaxLength(500)] public string? Description { get; set; }
    }
}
