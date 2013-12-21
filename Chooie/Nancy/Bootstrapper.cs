using Nancy;
using Nancy.Conventions;
using Nancy.TinyIoc;

namespace Chooie.Nancy
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        private readonly NancyContainerBuilder _nancyContainerBuilder;

        public Bootstrapper(NancyContainerBuilder nancyContainerBuilder)
        {
            _nancyContainerBuilder = nancyContainerBuilder;
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);

            nancyConventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory(@"", @"static"));
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            _nancyContainerBuilder.ConfigureContainer(container);
        }
    }
}
