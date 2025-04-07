// See https://aka.ms/new-console-template for more information
using Hippocrates.Journal.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

internal class Program {
    private static void Main(string[] args) {

        var services = CreateServiceCollection();
        //var ProductService = services.GetService<IProductService>() ?? throw new Exception("Unable to locate a valid Product Logic module");
        //var OrderService = services.GetService<IOrderService>() ?? throw new Exception("Unable to locate a valid Order Logic module");

        static IServiceProvider CreateServiceCollection() {
            var servicecollection = new ServiceCollection()
                .AddDbContext<IDatabaseContext, DatabaseContext>(options => {
                    options.UseSqlite($"Data Source={DatabaseContext.GetSqliteDbPath()}");
                })
                //.AddSingleton<IProductRepository, ProductRepository>()
                //.AddSingleton<IOrderRepository, OrderRepository>()
                //.AddSingleton<IProductService, ProductService>()
                //.AddSingleton<IOrderService, OrderService>()
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