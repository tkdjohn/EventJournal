using EventJournal.Common.Bootstrap;
using EventJournal.Common.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventJournal.BootStrap.Installers {
    public class JsonOptionsInstaller : IInstaller {
        public void Install(IServiceCollection services, IConfiguration configuration) {
            // make changes to JsonSerializerOptions in the static JsonSerializerSettings if needed
            services.AddSingleton(JsonSerializerSettings.JsonSerializerOptions);
        }
    }
}
