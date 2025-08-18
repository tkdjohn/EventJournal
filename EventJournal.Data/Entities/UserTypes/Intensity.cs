using EventJournal.Data.Entities.Enumerations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventJournal.Data.Entities.UserTypes {
    public class Intensity : BaseEntity {
        [Key]
        public new int Id { get { return base.Id; } set { base.Id = value; } }
        [Required]
        public new Guid ResourceId { get { return base.ResourceId; } set { base.ResourceId = value; } }

        [Required, MaxLength(50)]
        public required string Name { get; set; }

        [Required]
        public required int Level { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public required SortType DefaultSortType { get; set; }

        [ForeignKey(nameof(DetailTypeId))]
        [Required]
        public required int DetailTypeId { get; set; }

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
