using EventJournal.DomainDto.UserTypes;
using System.Text.Json;

namespace EventJournal.DomainDto {
    public static partial class DtoHelper {
        public static T UpdateEntity<T>(this T destination, T source) where T : BaseDto {
            destination.CopyUserValues(source);
            destination.CreatedDate = source.CreatedDate;
            destination.UpdatedDate = DateTime.UtcNow;
            return destination;
        }

        public static string Serialize<T>(this T entity) where T : BaseDto {
            //TODO: use reflection to find objects that inherit from BaseDto and their type 
            if (entity is DetailDto detail && detail != null) {
                return JsonSerializer.Serialize(detail);
            }
            if (entity is EventDto @event && @event != null) {
                return JsonSerializer.Serialize(@event);
            }
            if (entity is DetailTypeDto detailType && detailType != null) {
                return JsonSerializer.Serialize(detailType);
            }
            if (entity is EventTypeDto eventTypeDto && eventTypeDto != null) {
                return JsonSerializer.Serialize(eventTypeDto);
            }
            if (entity is IntensityDto intensityDto && intensityDto != null) {
                return JsonSerializer.Serialize(intensityDto);
            }
            return JsonSerializer.Serialize(entity);
        }

        public static T? Deserialize<T>(this string json) where T : BaseDto {
            //TODO: does this work properly for inherited types?
            return JsonSerializer.Deserialize<T>(json);
        }

        public static string ToString<T>(this T entity) where T : BaseDto {
            return entity.Serialize();
        }
    }
}
