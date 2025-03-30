using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hippocrates.Journal.DomainEntities {
    public class EventSymptom : BaseEntity{
        [Key] public int EventSymptomId { get; set; }
        [Required] public Guid EventSymptomGuid { get; set; }
        [Required] public virtual required Event Event { get; set; }
        [Required] public virtual required Symptom Symptom { get; set; }
        [Required] public virtual required Intensity Intensity { get; set; }
        [MaxLength(512)] public string? Notes { get; set; }
    }
}
