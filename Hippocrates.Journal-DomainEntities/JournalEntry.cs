using System.ComponentModel.DataAnnotations;

namespace Hippocrates.Journal_DomainEntities {
    public class JournalEntry : BaseEntity {
        [Key]
        public int JournalEntryId { get; set; }
        [Required]
        public Guid JournalEntryGuid { get; set; }
        [Required]
        public DateTime Timestamp { get; set; } = DateTime.Now;
        [Required]
        public virtual required Symptom Symptom { get; set; }
        //TODO: add other journal entry types or consider making separate tables
        // and just tracking a journal entry type here (food, exercise)
        public virtual required Intensity Intensity { get; set; }
        [MaxLength(512)]
        public string? JournalEntryDescription { get; set; }
    }
}
