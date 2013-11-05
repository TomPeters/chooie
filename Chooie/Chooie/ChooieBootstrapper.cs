using Nancy;
using Nancy.Conventions;
using Nancy.TinyIoc;
using Chooie.Core.PackageManager;
using Chooie.Jobs;
using Chooie.PackageManager;
using Chooie.SignalR;

namespace Chooie
{
    public class ChooieBootstrapper : DefaultNancyBootstrapper
    {
        private readonly PackageManagerSettings _packageManagerSettings;
        private readonly IPackageManagerProvider _packageManagerProvider;

        public ChooieBootstrapper(PackageManagerSettings packageManagerSettings,
                                IPackageManagerProvider packageManagerProvider)
        {
            _packageManagerSettings = packageManagerSettings;
            _packageManagerProvider = packageManagerProvider;
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);

            nancyConventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory(@"", @"static"));
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            container.Register<IJobFactory, JobFactory>();
            container.Register<IJobListUpdater, JobListUpdater>();
            container.Register<IJobQueue, JobQueue>().AsSingleton();

            container.Register<IClientMessenger, ClientMessenger>().AsSingleton();
            container.Register(_packageManagerSettings);
            var packageManagerProxy = new PackageManagerProxy(_packageManagerProvider, _packageManagerSettings, container.Resolve<IClientMessenger>(), container.Resolve<IJobQueue>());
            container.Register<IPackageManager>(packageManagerProxy);
            container.Register<IPackageManagerProxy>(packageManagerProxy);
            
        }
    }
}
