// See https://aka.ms/new-console-template for more information
using EventJournal.CLI;
using EventJournal.Data;
using EventJournal.Data.UserTypeRepositories;
using EventJournal.DomainService;
using EventJournal.PublicModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;
internal class Program {
    private static async Task Main(string[] args) {

        var services = CreateServiceCollection();
        var eventService = services.GetService<IEventService>() ?? throw new Exception("Unable to locate a valid Product Logic module");
        var userTypeService = services.GetService<IDetailService>() ?? throw new Exception("Unable to locate a valid Order Logic module");
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
            Console.WriteLine("Type 'a' to add some test data.");
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
                case 'a':
                    await AddTestDataAsync(eventService, userTypeService).ConfigureAwait(false);
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
        var servicecollection = new ServiceCollection()
            .AddDbContext<IDatabaseContext, DatabaseContext>(options => {
                options.UseSqlite($"Data Source={DatabaseContext.GetSqliteDbPath()}");
            })
            .AddAutoMapper(cfg => { }, typeof(DomainMapperProfile))
            .AddSingleton<IEventRepository, EventRepository>()
            .AddSingleton<IDetailRepository, DetailRepository>()
            .AddSingleton<IDetailTypeRepository, DetailTypeRepository>()
            .AddSingleton<IEventTypeRepository, EventTypeRepository>()
            .AddSingleton<IIntensityRepository, IntensityRepository>()
            .AddSingleton<IEventService, EventService>()
            .AddSingleton<IDetailService, DetailService>()
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
    static async Task AddTestDataAsync(IEventService eventService, IDetailService userTypeService) {
        Console.WriteLine("Adding/Resetting test data.");
        await eventService.AddTestDataAsync().ConfigureAwait(false);
        await userTypeService.AddTestDataAsync().ConfigureAwait(false);
    }
    static async Task DeleteAllDataAsync(IEventService eventService, IDetailService userTypeService) {

        Console.WriteLine("Deleting all data.");
        foreach (var e in await eventService.GetAllEventsAsync().ConfigureAwait(false)) {
            await eventService.DeleteEventAsync(e.ResourceId).ConfigureAwait(false);
        }
        foreach (var e in await eventService.GetAllEventTypesAsync().ConfigureAwait(false)) {
            await eventService.DeleteEventTypeAsync(e.ResourceId).ConfigureAwait(false);
        }
        foreach (var e in await userTypeService.GetAllDetailsAsync().ConfigureAwait(false)) {
            await userTypeService.DeleteDetailAsync(e.ResourceId).ConfigureAwait(false);
        }
        foreach (var e in await userTypeService.GetAllIntensitiesAsync().ConfigureAwait(false)) {
            await userTypeService.DeleteIntensityAsync(e.ResourceId).ConfigureAwait(false);
        }
        foreach (var e in await userTypeService.GetAllDetailTypesAsync().ConfigureAwait(false)) {
            await userTypeService.DeleteDetailTypeAsync(e.ResourceId).ConfigureAwait(false);
        }
    }

    //TODO: move to shared utilities class or bootstrapper class and remove this method
    private static JsonSerializerOptions GetSerializerOptions() {
        return new JsonSerializerOptions {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    static async Task ViewallDataAsync(JsonSerializerOptions options, IEventService eventService, IDetailService userTypeService) {
        EventDataResponseModel eventData = new() {
            DetailTypes = await userTypeService.GetAllDetailTypesAsync().ConfigureAwait(false),
            EventTypes = await eventService.GetAllEventTypesAsync().ConfigureAwait(false),
            Events = await eventService.GetAllEventsAsync().ConfigureAwait(false)
        };

        Console.WriteLine(JsonSerializer.Serialize(eventData, options));

    }
}