using System.ComponentModel.DataAnnotations;

namespace Hippocrates.Journal.DomainEntities {
    public class Symptom : BaseEntity {
        [Key] public int SymptomId { get { return Id; } set { Id = value; } }
        [Required] public Guid SymptomResourceId { get { return ResourceId; } set { ResourceId = value; } }
        [Required] public required string Name { get; set; }
        [MaxLength(500)] public string? Description { get; set; }
    }
}
