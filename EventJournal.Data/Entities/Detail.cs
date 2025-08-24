using EventJournal.Data.Entities.UserTypes;
using System.ComponentModel.DataAnnotations;

namespace EventJournal.Data.Entities {
    public class Detail : BaseEntity {
        [Key]
        public override int Id { get; set; }

        [Required]
        public override Guid ResourceId { get; set; }

        [Required]
        public required Event Event { get; set; }

        [Required]
        public required DetailType DetailType { get; set; }

        [Required]
        public required Intensity Intensity { get; set; }

        [MaxLength(512)]
        public string? Notes { get; set; }

        internal override void CopyUserValues<T>(T source) {
            var sourceDetail = source as Detail ?? throw new InvalidCastException($"{nameof(source)} is not of type {typeof(Detail)}");
            Event = sourceDetail.Event;
            DetailType = sourceDetail.DetailType;
            Intensity = sourceDetail.Intensity;
            Notes = sourceDetail.Notes;
        }
    }
}
