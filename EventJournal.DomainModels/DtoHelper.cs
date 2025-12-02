using EventJournal.Common.Configuration;
using System.Text.Json;

namespace EventJournal.DomainDto {
    public static partial class DtoHelper {
        //TODO: consider using AutoMapper for this if it is even needed
        public static T UpdateDTO<T>(this T destination, T source) where T : BaseDto {
            ArgumentNullException.ThrowIfNull(destination, nameof(destination));
            ArgumentNullException.ThrowIfNull(source, nameof(source));
            if (destination.ResourceId != source.ResourceId)
                //TODO: custom exception that supports a ThrowIf parameter?
                throw new InvalidOperationException("ResourceIds do not match");
            destination.CopyUserValues(source);
            destination.CreatedDate = source.CreatedDate;
            destination.UpdatedDate = DateTime.UtcNow;
            return destination;
        }

        public static string Serialize<T>(this T entity, JsonSerializerOptions? serializerOptions = null) where T : BaseDto {
            return JsonSerializer.Serialize(entity, entity.GetType(), serializerOptions ?? JsonSerializerSettings.JsonSerializerOptions);
        }

        public static string Serialize<T>(this IEnumerable<T> list, JsonSerializerOptions? serializerOptions = null) where T : BaseDto {
            return JsonSerializer.Serialize(list, list.GetType(), serializerOptions ?? JsonSerializerSettings.JsonSerializerOptions);
        }

        //TODO: options should be setup in bootstrap

        public static T? Deserialize<T>(this string json) where T : BaseDto {
            //TODO: does this work properly for inherited types?
            return JsonSerializer.Deserialize<T>(json);
        }

    }
}
