using System.ComponentModel.DataAnnotations;

namespace Hippocrates.Journal.DomainEntities {
    public class BaseEntity {
        [Required] public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required] public DateTime UpdatedDate { get; set; } = DateTime.MinValue;
    }
}
