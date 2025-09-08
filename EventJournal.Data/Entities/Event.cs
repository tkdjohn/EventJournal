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

        public IReadOnlyCollection<Detail> Details => (IReadOnlyCollection<Detail>)_details;
        private IList<Detail> _details = [];

        public Detail AddUpdateDetail(Detail detail) {
            ArgumentNullException.ThrowIfNull(detail, nameof(detail));
            var existingDetail = _details.FirstOrDefault(d =>  d.ResourceId == detail.ResourceId || (d.Id != 0 && d.Id == detail.Id));
            if (existingDetail != null) {
                //TODO: shouldn't this already be the case?
                existingDetail.Event = this;
                return existingDetail.UpdateEntity(detail);
            }
            detail.Event = this;
            _details.Add(detail);
            return detail;
        }

        public void RemoveDetail(Guid detailResourceId) {
            var existingDetail = _details.FirstOrDefault(d => d.ResourceId == detailResourceId);
            if (existingDetail != null) {
                _details.Remove(existingDetail);
            }
        }

        public void RemoveAllDetails() {
            _details.Clear();
        }

        internal override void CopyUserValues<T>(T source) {
            var soruceEvent = source as Event ?? throw new InvalidCastException($"{nameof(source)} is not of type {typeof(Event)}");
            EventType = soruceEvent.EventType;
            StartTime = soruceEvent.StartTime;
            EndTime = soruceEvent.EndTime;
            Description = soruceEvent.Description;
            _details = soruceEvent._details;
        }
    }
}