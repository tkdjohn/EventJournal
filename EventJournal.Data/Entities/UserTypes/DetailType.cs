using EventJournal.Common.Enumerations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventJournal.Data.Entities.UserTypes {
    public class DetailType : BaseEntity {
        [Key]
        public override int Id { get; set; }

        [Required]
        public override Guid ResourceId { get; set; }

        [Required]
        public required string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public SortType IntensitySortType { get; set; } = SortType.None;

        public IReadOnlyCollection<Intensity> AllowedIntensities => (IReadOnlyCollection<Intensity>)_intensities;
        private IList<Intensity> _intensities = [];

        public Intensity AddUpdateAllowedIntensity(Intensity intensity) {
            ArgumentNullException.ThrowIfNull(intensity, nameof(intensity));
            var existingIntensity = _intensities.FirstOrDefault(i => i.Id == intensity.Id || i.ResourceId == intensity.ResourceId);
            if (existingIntensity != null) {
                //TODO: shouldn't this already be the case?
                existingIntensity.DetailType = this;
                return existingIntensity.UpdateEntity(intensity);
            }
            intensity.DetailType = this;
            _intensities.Add(intensity);
            return intensity;
        }

        public void AddUpdateAllowedIntensities(IEnumerable<Intensity> intensities) {
            ArgumentNullException.ThrowIfNull(intensities, nameof(intensities));
            foreach (var intensity in intensities) { 
                AddUpdateAllowedIntensity(intensity); 
            }
        }

        public void RemoveIntensity(Guid intensityResoruceId) {
            var existignIntensity = _intensities.FirstOrDefault(i => i.ResourceId == intensityResoruceId);
            if (existignIntensity != null) {
                _intensities.Remove(existignIntensity);
            }
        }
        public void RemoveAllIntensities() {
            _intensities.Clear();
        }

        internal override void CopyUserValues<T>(T source) {
            var sourceDetailType = source as DetailType ?? throw new InvalidCastException($"{nameof(source)} is not of type {typeof(DetailType)}");
            Name = sourceDetailType.Name;
            Description = sourceDetailType.Description;
        }
    }
}
