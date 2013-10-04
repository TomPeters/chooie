using Core.PackageManager;
using Nancy;
using Nancy.Conventions;

namespace chui
{
    public class ChuiBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);

            nancyConventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory(@"", @"static"));
        }

        protected override void ConfigureApplicationContainer(Nancy.TinyIoc.TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register((c, p) => c.Resolve<IPackageManagerProvider>().GetPackageManager());
            container.Register<IPackageManagerProvider, PackageManagerProvider>().AsSingleton();
            container.Register<IPackageManagerFactoriesProvider, PackageManagerFactoriesProvider>().AsSingleton();
            container.Register<PackageManagerSettings>().AsSingleton();
            container.Register((c, p) => c.Resolve<IPackageManagerProvider>().GetPackageManager());
            container.Resolve<PackageManagerSettings>().PackageManagerType = "Chocolatey"; // TODO: Find a better way/place of doing this initialisation/settings loading
        }
    }
}
