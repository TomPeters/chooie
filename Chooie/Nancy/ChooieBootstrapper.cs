using Nancy;
using Nancy.Conventions;
using Nancy.TinyIoc;

namespace Chooie.Nancy
{
    public class ChooieBootstrapper : DefaultNancyBootstrapper
    {
        private readonly NancyDependencyContainerBuilder _nancyDependencyContainerBuilder;

        public ChooieBootstrapper(NancyDependencyContainerBuilder nancyDependencyContainerBuilder)
        {
            _nancyDependencyContainerBuilder = nancyDependencyContainerBuilder;
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);

            nancyConventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory(@"", @"static"));
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            _nancyDependencyContainerBuilder.ConfigureContainer(container);
        }
    }
}
