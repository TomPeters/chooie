using Nancy;
using Nancy.Conventions;
using Nancy.TinyIoc;
using chui.Core.PackageManager;
using chui.PackageManager;

namespace chui
{
    public class ChuiBootstrapper : DefaultNancyBootstrapper
    {
        private readonly PackageManagerSettings _packageManagerSettings;
        private readonly IPackageManagerProvider _packageManagerProvider;

        public ChuiBootstrapper(PackageManagerSettings packageManagerSettings,
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
            container.Register(_packageManagerSettings);
            container.Register((c, p) => _packageManagerProvider.GetPackageManager(c.Resolve<PackageManagerSettings>().PackageManagerType));
        }
    }
}
