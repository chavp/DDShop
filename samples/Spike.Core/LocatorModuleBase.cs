using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spike.Core {
    public abstract class LocatorModuleBase : Autofac.Module {

        public LocatorModuleBase() {
            var container = new Autofac.ContainerBuilder();
        }

    }
}
