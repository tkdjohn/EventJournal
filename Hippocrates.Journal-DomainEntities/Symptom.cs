using System.ComponentModel.DataAnnotations;

namespace Hippocrates.Journal_DomainEntities {
    public class Symptom : BaseEntity {
        [Key]
        public int SymptomId { get; set; }
        [Required]
        public Guid SymptomGuid { get; set; }
        [Required]
        public required string SymptomName { get; set; }
        [MaxLength(100)]
        public string? SymptomDescription { get; set; }
    }
}
