using EventJournal.BootStrap.Installers;
using EventJournal.Common.Bootstrap;

namespace EventJournal.BootStrap {
    public class DefaultApplicationBootStrapper : BootStrapper {
        public DefaultApplicationBootStrapper() {
            installers = [
                new RepositoryInstaller(),
                new DomainServiceInstaller(),
                new DistributedLockInstaller(),
            ];
        }
    }
}
