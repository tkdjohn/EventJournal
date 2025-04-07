using System.ComponentModel.DataAnnotations;

namespace Hippocrates.Journal.DomainEntities {
    public class EventSymptom : BaseEntity{
        [Key] public int EventSymptomId { get { return Id; } set { Id = value; } }
        [Required] public Guid EventSymptomResourceId { get { return ResourceId; } set { ResourceId = value; } }
        [Required] public virtual required Event Event { get; set; }
        [Required] public virtual required Symptom Symptom { get; set; }
        [Required] public virtual required Intensity Intensity { get; set; }
        [MaxLength(512)] public string? Notes { get; set; }
    }
}
