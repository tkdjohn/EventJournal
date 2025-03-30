using System.ComponentModel.DataAnnotations;

namespace Hippocrates.Journal.DomainEntities {
    public class Intensity :BaseEntity {
        [Key] public int IntensityId { get; set; }
        [Required] public Guid IntensityGuid { get; set; }
        [Required, MaxLength(50)] public required string Name { get; set; }
        [Required] public required int Level { get; set; }
        [MaxLength(500)] public string? Description { get; set; }
        [Required] public required SortType SortType { get; set; }
    }
    //TODO: move this somewhere more sensible 
    public enum SortType {
        Custom, Ascending, Descending
    }
}
