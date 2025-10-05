using System.Collections.ObjectModel;

namespace EventJournal.Common.Bootstrap {
    public class BootStrapperOptions {
        protected List<IInstaller> installers;

        public BootStrapperOptions() {
            installers = [];
        }

        public void AddInstaller(IInstaller installer) {
            installers.Add(installer);
        }

        public ReadOnlyCollection<IInstaller> Installers {
            get { return installers.AsReadOnly(); }
        }
    }
}
