using System.ComponentModel.DataAnnotations;

namespace Hippocrates.Journal.DomainEntities {
    public class Symptom : BaseEntity {
        [Key] public int SymptomId { get { return Id; } set { Id = value; } }
        [Required] public Guid SymptomResourceId { get { return ResourceId; } set { ResourceId = value; } }
        [Required] public required string SymptomName { get; set; }
        [MaxLength(500)] public string? SymptomDescription { get; set; }
    }
}
