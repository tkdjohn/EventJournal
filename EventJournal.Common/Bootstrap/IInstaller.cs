using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventJournal.Common.Bootstrap {
    public interface IInstaller {
        void Install(IServiceCollection services, IConfiguration configuration);
    }
}
