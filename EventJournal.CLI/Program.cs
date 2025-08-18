// See https://aka.ms/new-console-template for more information
using EventJournal.CLI;
using EventJournal.Data;
using EventJournal.Data.UserTypeRepositories;
using EventJournal.DomainDto;
using EventJournal.DomainService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
internal class Program {
    private static async Task Main(string[] args) {

        var services = CreateServiceCollection();
        var eventService = services.GetService<IEventService>() ?? throw new Exception("Unable to locate a valid Product Logic module");
        var userTypeService = services.GetService<IUserTypeService>() ?? throw new Exception("Unable to locate a valid Order Logic module");
        bool userIsDone = false;
        while (!userIsDone) {
            //Console.WriteLine("Type '1' to ");
            //Console.WriteLine("Type '2' to ");
            //Console.WriteLine("Type '3' to ");
            //Console.WriteLine("Type '4' to ");
            //Console.WriteLine("Type '6' to ");
            //Console.WriteLine("Type '7' to ");
            //Console.WriteLine("Type '8' to ");

            Console.WriteLine("Type '9' to view all data");
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
                case '8':

                    break;
                case '9':
                    await ViewallDataAsync().ConfigureAwait(false);
                    break;
                case 'a':
                    await AddTestDataAsync(eventService, userTypeService).ConfigureAwait(false);
                    break;
                case 'x':
                    await DeleteAllDataAsync(eventService, userTypeService).ConfigureAwait(false);
                    break;
            }
            Console.WriteLine("\n===============================\n");
        }
        //TODO: move to shared startup.cs and remove this method and remove microsoft.extension.hosting pkg
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
        async Task AddTestDataAsync(IEventService eventService, IUserTypeService userTypeService) {
            Console.WriteLine("Adding/Resetting test data.");
            await AddUpdateDtoAsync(EventDto.CreateDefaultEventDto()).ConfigureAwait(false);
            //await AddUpdateEntity(new Product { ProductId = 2, Name = "Dry Cat Food", Quantity = 0, Price = 15.99M }).ConfigureAwait(false);
            //await AddUpdateEntity(new Product { ProductId = 100, Name = "Designer Leash", Quantity = 1, Price = 99.99M }).ConfigureAwait(false);
            //await AddUpdateEntity(new Order { OrderId = 1, OrderDate = DateTime.Now, OrderProducts = { new OrderProduct { ProductId = 100, OrderQuantity = 5, UnitPrice = 99.99M } } }).ConfigureAwait(false);
            //await AddUpdateEntity(new Order { OrderId = 2, OrderDate = DateTime.Now, OrderProducts = { new OrderProduct { ProductId = 2, OrderQuantity = 3, UnitPrice = 15.99M } } }).ConfigureAwait(false);
        }
        static async Task DeleteAllDataAsync(IEventService eventService, IUserTypeService userTypeService) {
            var events = await eventService.GetAllEventsAsync().ConfigureAwait(false);
            foreach (var e in events) {
                await eventService.DeleteEventAsync(e.EventResourceId).ConfigureAwait(false);
            }
            //var orders =( await userTypeService.GetOrdersAsync().ConfigureAwait(false)).ToList();
            //orders.ForEach(async o => await orderService.RemoveOrderAsync(o).ConfigureAwait(false));
        }
        async Task ViewallDataAsync() {
            var events = await eventService.GetAllEventsAsync().ConfigureAwait(false);
            foreach (var e in events) {
                Console.WriteLine(e.Serialize());
            } 
        }
        //static T? GetEntityFromUser<T>() where T : EntityBase {
        //    var json = CLIUtilities.GetStringFromUser($"Enter {typeof(T)} JSON: ");
        //    T? entity = json.Deserialize<T>();
        //    if (entity == null) {
        //        Console.WriteLine("Invalid JSON.");
        //        return null;
        //    }
        //    return entity;
        //}

        //static bool ValidateEntity<T>(T entity) where T : EntityBase {
        //    var validationResult = entity.Validate<T>();
        //    if (!validationResult.IsValid) {
        //        Console.WriteLine("Invalid Product data.");
        //        foreach (var error in validationResult.Errors) {
        //            Console.WriteLine(error);
        //        }
        //        return false;
        //    }
        //    return true;
        //}

        //async Task AddEntity<T>(T? newEntity) where T : EntityBase {
        //    if (newEntity == null) {
        //        Console.WriteLine("Nothing added.");
        //        return;
        //    }
        //    if (!ValidateEntity<T>(newEntity)) {
        //        return;
        //    }

        //    if (newEntity is Product newProduct) {
        //        await ProductService.AddProductAsync(newProduct);
        //        Console.WriteLine($"Added {newProduct.Name}.");
        //        return;
        //    }

        //    if (newEntity is Order newOrder) {
        //        await OrderService.AddOrderAsync(newOrder);
        //        Console.WriteLine($"Added order # {newOrder.OrderId}.");
        //    }
        //}

        //async Task ViewProduct() {
        //    var name = CLIUtilities.GetStringFromUser("Enter the product name you want to view: ");
        //    var product = await ProductService.GetProductAsync(name).ConfigureAwait(false);
        //    if (product == null) {
        //        Console.WriteLine($"The product '{name}' was not found.\n");
        //        return;
        //    }
        //    Console.WriteLine(product.Serialize());
        //    Console.WriteLine();
        //}

        //async Task ViewOrder() {
        //    var orderId = CLIUtilities.GetIntFromUser("Enter the order id you want to view: ");
        //    var order = await OrderService.GetOrderAsync(orderId).ConfigureAwait(false);
        //    if (order == null) {
        //        Console.WriteLine($"Order Id {orderId} was not found.\n");
        //        return;
        //    }
        //    Console.WriteLine(order.ToString());
        //    Console.WriteLine();
        //}


        //async Task ViewInStockProducts() {
        //    var inStock = await ProductService.GetInStockProductsAsync().ConfigureAwait(false);
        //    Console.WriteLine("The following products are in stock: ");
        //    inStock.ForEach(p => Console.WriteLine(p.Serialize()));
        //}

        //async Task ViewAllProduct() {
        //    var products = await ProductService.GetProductsAsync().ConfigureAwait(false);
        //    products.ForEach(p => Console.WriteLine(p.Serialize()));
        //}

        async Task AddUpdateDtoAsync<T>(T? dtoUpdate) where T : BaseDto {

            if (dtoUpdate == null) {
                Console.WriteLine("Nothing updated.");
                return;
            }

            //if (!ValidateEntity(entityUpdate)) {
            //    return;
            //}

            if (dtoUpdate is EventDto @event) {
                await eventService.AddUpdateEventAsync(@event);
            }
        }

    }
}