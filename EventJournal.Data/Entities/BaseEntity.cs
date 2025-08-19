using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventJournal.Data.Entities {
    public abstract class BaseEntity {
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        
        [Required]
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        
        [NotMapped]
        [Required]
        public int Id { get; set; }
        
        [NotMapped]
        [Required]
        public Guid ResourceId { get; set; }

        /// <summary>
        /// This method is predominantly for updating an entity based on the values in another entity.
        /// Ids and audit info (created, last modified, etc) should NOT be copied!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        internal abstract void CopyUserValues<T>(T source);
    }
}
