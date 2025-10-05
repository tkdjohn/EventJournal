using EventJournal.Common.Bootstrap;
using EventJournal.DomainService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventJournal.BootStrap.Installers {
    public class ModelMapperInstaller :IInstaller {
        public void Install(IServiceCollection services, IConfiguration configuration) {
            services.AddAutoMapper(cfg => { }, typeof(DomainMapperProfile));
        }
    }
}
