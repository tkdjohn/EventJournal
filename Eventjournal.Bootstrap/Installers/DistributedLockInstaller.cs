using EventJournal.Common.Bootstrap;
using EventJournal.Configuration;
using EventJournal.Data;
using Medallion.Threading;
using Medallion.Threading.MySql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventJournal.BootStrap.Installers {
    public class DistributedLockInstaller : IInstaller {
        public void Install(IServiceCollection services, IConfiguration configuration) {
            var dbSettings = configuration.GetSection(DatabaseSettings.ConfigurationSectionName).Get<DatabaseSettings>()
                ?? throw new InvalidOperationException("Configuration settings does not contain a valid DatabaseSettings section.");


            IDistributedLockProvider provider = new MySqlDistributedSynchronizationProvider(DatabaseContext.GetConnectionString(dbSettings));
            services.AddSingleton(provider);
        }
    }
}
