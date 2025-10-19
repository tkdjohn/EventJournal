using System.Text.Json;
using System.Text.Json.Serialization;

namespace EventJournal.Common.Configuration {
    // this class is static so helper methods can consume it without dependency injection
    // if you need a instantiated version, use JsonSerailizerOptions installed via bootstrap
    public static class JsonSerializerSettings {
        public static JsonSerializerOptions JsonSerializerOptions { get; set; } = new() {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = {
                new JsonStringEnumConverter()
                // TODO: add additional converters as needed
            }
        };
    }
}
