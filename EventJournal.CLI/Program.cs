// See https://aka.ms/new-console-template for more information
using EventJournal.BootStrap;
using EventJournal.CLI;
using EventJournal.Common.Bootstrap;
using EventJournal.Data;
using EventJournal.Data.UserTypeRepositories;
using EventJournal.DomainDto;
using EventJournal.DomainService;
using EventJournal.PublicModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;
internal class Program {
    private static async Task Main(string[] args) {

        var services = CreateServiceCollection();
        var eventService = services.GetService<IEventService>() ?? throw new Exception("Unable to locate a valid Event Service");
        var userTypeService = services.GetService<IUserTypesService>() ?? throw new Exception("Unable to locate a valid User Types Service");
        var defaultDataProvider = services.GetService<IDefaultDataProvider>() ?? throw new Exception("Unable to locate a valid Default Data Provider");
        bool userIsDone = false;
        while (!userIsDone) {
            //Console.WriteLine("Type '1' to ");
            //Console.WriteLine("Type '2' to ");
            //Console.WriteLine("Type '3' to ");
            //Console.WriteLine("Type '4' to ");
            //Console.WriteLine("Type '6' to ");
            //Console.WriteLine("Type '7' to ");
            //Console.WriteLine("Type '8' to ");

            Console.WriteLine("Type 'v' to view all data");
            Console.WriteLine("Type 't' to add some test data.");
            Console.WriteLine("Type 'x' to delete all data.");
            Console.WriteLine("Type 'q' to quit.");

            // application will block here waiting for user to press <Enter>
            var userInput = CLIUtilities.GetStringFromUser("===> ").ToLower() ?? "";

            switch (userInput[0]) {
                case 'q':
                    userIsDone = true;
                    break;
                //    case '1':
                //        await AddUpdateEntity(GetEntityFromUser<Product>()).ConfigureAwait(false);
                //        break;
                //    case '2':
                //        await ViewProduct().ConfigureAwait(false);
                //        break;
                //    case '3':
                //        await ViewInStockProducts().ConfigureAwait(false);
                //        break;
                //    case '4':
                //        await ViewAllProduct().ConfigureAwait(false);
                //        break;
                //    case '5':

                //        break;
                //    case '6':
                //        await AddUpdateEntity(GetEntityFromUser<Order>()).ConfigureAwait(false);
                //        break;
                //    case '7':
                //        await ViewOrder().ConfigureAwait(false);
                //        break;
                //case '8':
                //    break;
                //case '9':
                //    break;
                case 'v':
                    await ViewallDataAsync(GetSerializerOptions(), eventService, userTypeService).ConfigureAwait(false);
                    break;
                case 't':
                    await AddTestDataAsync(defaultDataProvider).ConfigureAwait(false);
                    break;
                case 'x':
                    await DeleteAllDataAsync(eventService, userTypeService).ConfigureAwait(false);
                    break;
            }
            Console.WriteLine("\n=================================================\n");
        }
    }
    //TODO: move to shared bootsrapper.cs and remove this method and remove microsoft.extension.hosting pkg
    static IServiceProvider CreateServiceCollection() {
        IConfiguration Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var servicecollection = new ServiceCollection()
                //TOOD: move to bootstrapper and data source should be in appsettings.json
                .AddDbContext<IDatabaseContext, DatabaseContext>(options => {
                    options.UseSqlite($"Data Source={DatabaseContext.GetSqliteDbPath()}");
                })
                //TODO: most/all of these should in bootstrapper (and may already be there)
                .AddSingleton<IDefaultDataProvider, DefaultDataProvider>()
                .AddLogging(options => {
                    options.AddDebug();
                    options.SetMinimumLevel(LogLevel.Error);
                    options.AddSimpleConsole(options => {
                        options.SingleLine = true;
                        options.TimestampFormat = "HH:mm:ss.fff ";
                        options.ColorBehavior = Microsoft.Extensions.Logging.Console.LoggerColorBehavior.Enabled;
                    });
                })
                // setup and register boostrapper and it's installers -- needs to be last
                .AddBootStrapper<DefaultApplicationBootStrapper>(Configuration, o => {
                    //TOOD: add any application specific installers here if needed
                    // installers that are common to all applications should go in the DefaultApplicationBootStrapper class
                });
        return servicecollection.BuildServiceProvider();
    }

    static Task AddTestDataAsync(IDefaultDataProvider defaultDataProvider) {
        Console.WriteLine("Adding/Resetting test data.");
        return defaultDataProvider.AddResetDefaultDataAsync();
    }
    static async Task DeleteAllDataAsync(IEventService eventService, IUserTypesService userTypeService) {

        Console.WriteLine("Deleting all data.");
        foreach (var e in await eventService.GetAllEventsAsync().ConfigureAwait(false)) {
            await eventService.DeleteEventAsync(e.ResourceId).ConfigureAwait(false);
        }
        foreach (var e in await userTypeService.GetAllEventTypesAsync().ConfigureAwait(false)) {
            await userTypeService.DeleteEventTypeAsync(e.ResourceId).ConfigureAwait(false);
        }
        foreach (var e in await userTypeService.GetAllDetailTypesAsync().ConfigureAwait(false)) {
            await userTypeService.DeleteDetailTypeAsync(e.ResourceId).ConfigureAwait(false);
        }
    }

    //TODO: move to shared utilities class or bootstrapper class and remove this method
    private static JsonSerializerOptions GetSerializerOptions() {
        return DtoHelper.DefaultSerializerOptions;
    }

    static async Task ViewallDataAsync(JsonSerializerOptions options, IEventService eventService, IUserTypesService userTypeService) {
        EventDataResponseModel eventData = new() {
            DetailTypes = await userTypeService.GetAllDetailTypesAsync().ConfigureAwait(false),
            EventTypes = await userTypeService.GetAllEventTypesAsync().ConfigureAwait(false),
            Events = await eventService.GetAllEventsAsync().ConfigureAwait(false)
        };

        Console.WriteLine(JsonSerializer.Serialize(eventData, options));

    }
}