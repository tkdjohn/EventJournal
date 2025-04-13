using Hippocrates.Journal.DomainEntities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hippocrates.Journal.DomainEntities {
    public class Intensity :BaseEntity {
        [Key] public int IntensityId { get { return Id; } set { Id = value; } }
        [Required] public Guid IntensityResourceId { get { return ResourceId; } set { ResourceId = value; } }
        [Required, MaxLength(50)] public required string Name { get; set; }
        [Required] public required int Level { get; set; }
        [MaxLength(500)] public string? Description { get; set; }
        [Required] public required SortType DefaultSortType { get; set; }
        [ForeignKey(nameof(SymptomId))]
        [Required] public required int SymptomId { get; set; }
    }

}
