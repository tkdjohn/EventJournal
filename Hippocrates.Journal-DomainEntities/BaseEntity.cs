using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hippocrates.Journal.DomainEntities {
    public abstract class BaseEntity {
        [Required] public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required] public DateTime UpdatedDate { get; set; } = DateTime.MinValue;
        [NotMapped]
        [Required] public int Id { get; set; }
        [NotMapped]
        [Required] public Guid ResourceId { get; set; }
    }
}
