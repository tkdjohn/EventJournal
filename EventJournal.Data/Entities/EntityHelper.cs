namespace EventJournal.Data.Entities {
    public static class EntityHelper {
        public static T UpdateEntity<T>(this T destination, T source) where T : BaseEntity {
            ArgumentNullException.ThrowIfNull(destination);
            ArgumentNullException.ThrowIfNull(source);
            if (destination.ResourceId != source.ResourceId)
                //TODO: custom exception that supports a ThrowIf parameter?
                throw new InvalidOperationException($"{typeof(T).Name} ResourceIds do not match");
            destination.CopyUserValues(source);
            destination.UpdatedDate = DateTime.UtcNow;
            return destination;
        }
    }
}
