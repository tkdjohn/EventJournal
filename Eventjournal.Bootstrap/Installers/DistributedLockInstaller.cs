using EventJournal.Common.Bootstrap;
using Medallion.Threading;
using Medallion.Threading.MySql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventJournal.BootStrap.Installers {
    public class DistributedLockInstaller : IInstaller {
        public void Install(IServiceCollection services, IConfiguration configuration) {
            var connectionString = configuration.GetSection("Database").GetValue<string>("ConnectionString") 
                ?? throw new InvalidOperationException("Database:ConnectionString is not configured");
            IDistributedLockProvider provider = new MySqlDistributedSynchronizationProvider(connectionString);
            services.AddSingleton(provider);
        }
    }
}
