using EventJournal.Common.Bootstrap;
using EventJournal.DomainService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventJournal.BootStrap.Installers {
    public class DomainServiceInstaller : IInstaller {
        public void Install(IServiceCollection services, IConfiguration configuration) {
            //TODO: does this work with multiple services? suspect it does
            services.AddScopedInterfacesBySuffix<EventService>("Service");
        }
    }
}
