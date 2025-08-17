using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventJournal.DomainEntities {
    public abstract class BaseEntity {
        [Required] public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        [Required] public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        [NotMapped]
        [Required] public int Id { get; set; }
        [NotMapped]
        [Required] public Guid ResourceId { get; set; }

        /// <summary>
        /// This method is predominantly for updating an entity based on the values in another entity.
        /// Copy user values (ids and audit info (created, last modified, etc) should NOT be copied!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        internal abstract void CopyUserValues<T>(T source);
   }

    public static partial class EntityHelpers {
        public static T UpdateEntity<T>(this T destination, T source) where T : BaseEntity {
            destination.CopyUserValues(source);
            destination.CreatedDate = source.CreatedDate;
            destination.UpdatedDate = DateTime.UtcNow;
            return destination;
        }
    }
}
