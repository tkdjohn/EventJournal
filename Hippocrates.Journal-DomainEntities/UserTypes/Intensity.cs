using EventJournal.DomainEntities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventJournal.DomainEntities.UserTypes {
    public class Intensity : BaseEntity {
        [Key] public int IntensityId { get { return Id; } set { Id = value; } }
        [Required] public Guid IntensityResourceId { get { return ResourceId; } set { ResourceId = value; } }
        [Required, MaxLength(50)] public required string Name { get; set; }
        [Required] public required int Level { get; set; }
        [MaxLength(500)] public string? Description { get; set; }
        [Required] public required SortType DefaultSortType { get; set; }
        [ForeignKey(nameof(DetailTypeId))]
        [Required] public required int DetailTypeId { get; set; }

        internal override void CopyUserValues<T>(T source) {
            var sourceIntensity = source as Intensity ?? throw new InvalidCastException($"{nameof(source)} is not of type {typeof(Intensity)}");
            Name = sourceIntensity.Name;
            Level = sourceIntensity.Level;
            Description = sourceIntensity.Description;
            DefaultSortType = sourceIntensity.DefaultSortType;
            DetailTypeId = sourceIntensity.DetailTypeId;
        }
    }

}
