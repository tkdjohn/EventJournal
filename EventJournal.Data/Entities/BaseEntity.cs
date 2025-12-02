using System.ComponentModel.DataAnnotations;

namespace EventJournal.Data.Entities {
    public abstract class BaseEntity {
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

        [Required]
        public abstract int Id { get; set; }

        [Required]
        public abstract Guid ResourceId { get; set; }

        /// <summary>
        /// This method is predominantly for updating an entity based on the values in another entity.
        /// Ids and audit info (created, last modified, etc) should NOT be copied!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        internal abstract void CopyUserValues<T>(T source);
    }
}
