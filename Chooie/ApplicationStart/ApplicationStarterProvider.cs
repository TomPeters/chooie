using System.Collections.Generic;
using Chooie.Database;
using Chooie.Interface.TinyIoC;
using Chooie.Nancy;
using Chooie.Plugin;
using Chooie.SignalR;
using Chooie.SystemTrayApplication;

namespace Chooie.ApplicationStart
{
    public class ApplicationStarterProvider
    {
        private readonly TinyIoCContainer _container;

        public ApplicationStarterProvider(TinyIoCContainer container)
        {
            _container = container;
        }

        public IEnumerable<IStarter> Starters
        {
            get
            {
                yield return _container.Resolve<PluginStarter>();
                yield return _container.Resolve<SignalRStarter>();
                yield return _container.Resolve<DatabaseStarter>();
                yield return _container.Resolve<NancyStarter>();
                yield return _container.Resolve<SystemTrayApplicationStarter>();
            }
        }
    }
}
