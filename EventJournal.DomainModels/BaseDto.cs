namespace EventJournal.DomainDto {
    public abstract class BaseDto {

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

        public Guid ResourceId { get; set; }

        /// <summary>
        /// This method is predominantly for updating an entity based on the values in another entity.
        /// Copy user values (ids and audit info (created, last modified, etc) should NOT be copied!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        internal abstract void CopyUserValues<T>(T source);

    }

    //TODO: is this needed?
    public static partial class DtoHelpers {
        public static T UpdateEntity<T>(this T destination, T source) where T : BaseDto {
            destination.CopyUserValues(source);
            destination.CreatedDate = source.CreatedDate;
            destination.UpdatedDate = DateTime.UtcNow;
            return destination;
        }
    }
}