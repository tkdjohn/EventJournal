using EventJournal.BootStrap.Installers;
using EventJournal.Common.Bootstrap;

namespace EventJournal.BootStrap {
    public class DefaultApplicationBootStrapper : BootStrapper {
        public DefaultApplicationBootStrapper() {
            installers = [
                new DatabaseContextInstaller(),
                new DefaultDataProviderInstaller(),
                new DistributedLockInstaller(),
                new DomainServiceInstaller(),
                new JsonOptionsInstaller(),
                new LoggingInstaller(),
                new MiniProfilerInstaller(),
                new ModelMapperInstaller(),
                new RepositoryInstaller(),
            ];
        }
    }
}
