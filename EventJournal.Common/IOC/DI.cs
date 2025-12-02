using Microsoft.Extensions.Configuration;

namespace EventJournal.Common.IOC {
    public static class DI {
        private static readonly Lock lockObject = new();

        public static void SetContainer(IServiceProvider instance) {
            lock (lockObject) {
                Container = instance;
            }
        }

        public static IServiceProvider? Container { get; private set; }

        public static void SetConfiguration(IConfiguration instance) {
            lock (lockObject) {
                Configuration = instance;
            }
        }

        public static IConfiguration? Configuration { get; private set; }
    }
}
