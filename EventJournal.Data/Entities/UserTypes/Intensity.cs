using System.ComponentModel.DataAnnotations;

namespace EventJournal.Data.Entities.UserTypes {
    public class Intensity : BaseEntity {
        [Key]
        public override int Id { get; set; }
        [Required]
        public override Guid ResourceId { get; set; }

        [Required, MaxLength(50)]
        public required string Name { get; set; }

        [Required]
        public required int Level { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public required DetailType DetailType { get; set; }

        internal override void CopyUserValues<T>(T source) {
            var sourceIntensity = source as Intensity ?? throw new InvalidCastException($"{nameof(source)} is not of type {typeof(Intensity)}");
            Name = sourceIntensity.Name;
            Level = sourceIntensity.Level;
            Description = sourceIntensity.Description;
            DetailType.CopyUserValues( sourceIntensity.DetailType);
        }
    }

}
