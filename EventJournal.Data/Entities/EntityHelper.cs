namespace EventJournal.Data.Entities {
    public static class EntityHelper {
        public static T UpdateEntity<T>(this T destination, T source) where T : BaseEntity {
            destination.CopyUserValues(source);
            destination.CreatedDate = source.CreatedDate;
            destination.UpdatedDate = DateTime.UtcNow;
            return destination;
        }
    }
}
