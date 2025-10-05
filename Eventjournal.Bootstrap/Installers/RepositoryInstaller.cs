using EventJournal.Common.Bootstrap;
using EventJournal.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventJournal.BootStrap.Installers {
    public class RepositoryInstaller : IInstaller {
        public void Install(IServiceCollection services, IConfiguration configuration) {
            //TODO: does this work with multiple repositories? suspect it does
            services.AddScopedInterfacesBySuffix<EventRepository>("Repository");
        }
    }
}
