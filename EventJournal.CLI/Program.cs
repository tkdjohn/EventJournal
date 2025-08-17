// See https://aka.ms/new-console-template for more information
using EventJournal.Data;
using EventJournal.DomainService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

internal class Program {
    private static void Main(string[] args) {

        var services = CreateServiceCollection();
        var EventService = services.GetService<IEventService>() ?? throw new Exception("Unable to locate a valid Product Logic module");
        var UserTypeServes = services.GetService<IUserTypeService>() ?? throw new Exception("Unable to locate a valid Order Logic module");

        //TODO: move to shared startup.cs and remove this method and remove microsoft.extension.hosting pkg
        static IServiceProvider CreateServiceCollection() {
            var servicecollection = new ServiceCollection()
                .AddDbContext<IDatabaseContext, DatabaseContext>(options => {
                    options.UseSqlite($"Data Source={DatabaseContext.GetSqliteDbPath()}");
                })
                .AddSingleton<IEventRepository, EventRepository>()
                .AddSingleton<IDetailRepository, DetailRepository>()
                .AddSingleton<IEventService, EventService>()
                .AddSingleton<IUserTypeService, UserTypeService>()
                .AddLogging(options => {
                    options.AddDebug();
                    options.SetMinimumLevel(LogLevel.Error);
                    options.AddSimpleConsole(options => {
                        options.SingleLine = true;
                        options.TimestampFormat = "HH:mm:ss.fff ";
                        options.ColorBehavior = Microsoft.Extensions.Logging.Console.LoggerColorBehavior.Enabled;
                    });
                });


            return servicecollection.BuildServiceProvider();
        }
    }
}