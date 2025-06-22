using EventJournal.DomainEntities.UserTypes;
using System.ComponentModel.DataAnnotations;

namespace EventJournal.DomainEntities {
    public class Event : BaseEntity {
        [Key] public int EventId { get { return Id; } set { Id = value; } }
        [Required] public Guid EventResourceId { get { return ResourceId; } set { ResourceId = value; } }
        [Required] public EventType Type { get; set; } = EventType.GetDefaultEventType();
        [Required] public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime? EndTime { get; set; } = null;
        [MaxLength(500)] public string? Description { get; set; }

        public IEnumerable<Detail> Details { get; set; } = [];

        internal override void CopyUserValues<T>(T source) {
            var soruceEvent = source as Event ?? throw new InvalidCastException($"{nameof(source)} is not of type {typeof(Event)}");
            Type = soruceEvent.Type;
            StartTime = soruceEvent.StartTime;
            EndTime = soruceEvent.EndTime;
            Description = soruceEvent.Description;
            Details = soruceEvent.Details;
        }
    }
}