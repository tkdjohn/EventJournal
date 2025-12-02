using EventJournal.Common.Bootstrap;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EventJournal.BootStrap.Installers {
    public class LoggingInstaller : IInstaller {
        public void Install(IServiceCollection services, IConfiguration configuration) {
            // TODO: Make logging configuration driven eg. build options from config
            services.AddLogging(options => {
                options.AddDebug();
                options.SetMinimumLevel(LogLevel.Error);
                options.AddSimpleConsole(options => {
                    options.SingleLine = true;
                    options.TimestampFormat = "HH:mm:ss.fff ";
                    options.ColorBehavior = Microsoft.Extensions.Logging.Console.LoggerColorBehavior.Enabled;
                });
            });
        }
    }
}
