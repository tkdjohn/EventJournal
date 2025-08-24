using EventJournal.Data.Entities.UserTypes;
using System.ComponentModel.DataAnnotations;

namespace EventJournal.Data.Entities {
    public class Event : BaseEntity {
        [Key]
        public override int Id { get; set; }

        [Required]
        public override Guid ResourceId { get; set; }

        [Required]
        public required EventType EventType { get; set; }

        [Required]
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime? EndTime { get; set; } = null;

        [MaxLength(500)]
        public string? Description { get; set; }

        public ICollection<Detail> Details { get; set; } = [];

        internal override void CopyUserValues<T>(T source) {
            var soruceEvent = source as Event ?? throw new InvalidCastException($"{nameof(source)} is not of type {typeof(Event)}");
            EventType.UpdateEntity(soruceEvent.EventType);
            StartTime = soruceEvent.StartTime;
            EndTime = soruceEvent.EndTime;
            Description = soruceEvent.Description;
            Details = soruceEvent.Details;
        }
    }
}