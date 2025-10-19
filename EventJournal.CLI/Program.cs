// See https://aka.ms/new-console-template for more information
using EventJournal.BootStrap;
using EventJournal.CLI;
using EventJournal.Common.Bootstrap;
using EventJournal.DomainService;
using EventJournal.PublicModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
internal class Program {
    private static async Task Main(string[] args) {

        var services = CreateServiceCollection();
        var eventService = services.GetService<IEventService>() ?? throw new Exception("Unable to locate a valid Event Service");
        var userTypeService = services.GetService<IUserTypesService>() ?? throw new Exception("Unable to locate a valid User Types Service");
        var defaultDataProvider = services.GetService<IDefaultDataProvider>() ?? throw new Exception("Unable to locate a valid Default Data Provider");
        var jsonSerializerOptions = services.GetService<JsonSerializerOptions>() ?? throw new Exception("Unable to locate a valid Json Serializer Options");
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
            Console.WriteLine("Type 't' to add default test data.");
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
                    await ViewallDataAsync(jsonSerializerOptions, eventService, userTypeService).ConfigureAwait(false);
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

    private static ServiceProvider CreateServiceCollection() {
        IConfiguration Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        
        return new ServiceCollection()
                // setup and register bootstrapper and it's installers -- needs to be last
                .AddBootStrapper<DefaultApplicationBootStrapper>(Configuration, o => {
                    // Add any application specific installers here.
                }).BuildServiceProvider();
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

    static async Task ViewallDataAsync(JsonSerializerOptions options, IEventService eventService, IUserTypesService userTypeService) {
        EventDataResponseModel eventData = new() {
            DetailTypes = await userTypeService.GetAllDetailTypesAsync().ConfigureAwait(false),
            EventTypes = await userTypeService.GetAllEventTypesAsync().ConfigureAwait(false),
            Events = await eventService.GetAllEventsAsync().ConfigureAwait(false)
        };

        Console.WriteLine(JsonSerializer.Serialize(eventData, options));
    }
}