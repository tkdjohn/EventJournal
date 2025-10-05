using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EventJournal.Common.Bootstrap {
    public static class ServiceCollectionExtensions {
        /// <summary>
        /// Add scoped classes with specified suffix from assembly that contains T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static IServiceCollection AddScopedInterfacesBySuffix<T>(this IServiceCollection services, string suffix) where T : class {
            typeof(T).GetTypeInfo().Assembly.GetTypes()
                .Where(x => x.Name.EndsWith(suffix, StringComparison.InvariantCulture)
                            && x.GetTypeInfo().IsClass
                            && !x.GetTypeInfo().IsAbstract
                            && x.GetInterfaces().Length > 0)
                .ToList().ForEach(x => {
                    x.GetInterfaces().ToList()
                        .ForEach(i => services.AddScoped(i, x));
                });

            return services;
        }

        public static IServiceCollection AddSingletonClassesBySuffix<T>(this IServiceCollection services, string suffix) where T : class {
            typeof(T).GetTypeInfo().Assembly.GetTypes()
                .Where(x => x.Name.EndsWith(suffix, StringComparison.InvariantCulture)
                    && x.GetTypeInfo().IsClass
                    && !x.GetTypeInfo().IsAbstract)
                .ToList()
                .ForEach(x => services.AddSingleton(x));

            return services;
        }

        public static IServiceCollection AddBootStrapper<T>(this IServiceCollection services,
            IConfiguration configuration, Action<BootStrapperOptions> options) where T : BootStrapper, new() {
            services.AddSingleton(configuration);
            var bootstrapper = new T();

            var o = new BootStrapperOptions();
            options?.Invoke(o);

            foreach (var installer in o.Installers) {
                bootstrapper.AddInstaller(installer);
            }

            //TODO: figure out if we need to cast to IConfigurationRoot
            //bootstrapper.InitIoCContainer(configuration: (configuration as IConfigurationRoot), services);
            bootstrapper.InitIoCContainer(configuration, services);
            return services;
        }
    }
}
