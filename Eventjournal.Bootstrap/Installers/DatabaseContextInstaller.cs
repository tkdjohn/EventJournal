using EventJournal.Common.Bootstrap;
using EventJournal.Configuration;
using EventJournal.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventJournal.BootStrap.Installers {
    public class DatabaseContextInstaller : IInstaller {
        public void Install(IServiceCollection services, IConfiguration configuration) {

            var dbSettings = configuration.GetSection(DatabaseSettings.ConfigurationSectionName).Get<DatabaseSettings>() 
                ?? throw new InvalidOperationException("Configuration settings does not contain a valid DatabaseSettings section.");
            services.AddSingleton<DatabaseSettings>(dbSettings);
            services.AddDbContext<IDatabaseContext, DatabaseContext>(options => {
                options.UseSqlite(DatabaseContext.GetConnectionString(dbSettings));
            });
        }
    }
}
