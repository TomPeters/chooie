using Nancy;
using Nancy.Conventions;
using Nancy.TinyIoc;

namespace Chooie
{
    public class ChooieBootstrapper : DefaultNancyBootstrapper
    {
        private readonly DependencyContainerBuilder _dependencyContainerBuilder;

        public ChooieBootstrapper(DependencyContainerBuilder dependencyContainerBuilder)
        {
            _dependencyContainerBuilder = dependencyContainerBuilder;
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);

            nancyConventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory(@"", @"static"));
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            _dependencyContainerBuilder.ConfigureContainer(container);
        }
    }
}
