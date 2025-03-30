using System.ComponentModel.DataAnnotations;

namespace Hippocrates.Journal.DomainEntities {
    public class Event : BaseEntity {
        [Key] public int EventId { get; set; }
        [Required] public Guid EventGuid { get; set; }
        [Required] public DateTime Timestamp { get; set; } = DateTime.Now;
        [MaxLength(500)] public string? Description { get; set; }
    }
}
