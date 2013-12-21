using Nancy.Hosting.Self;

namespace Chooie.Nancy
{
    public class NancyHostProvider
    {
        private readonly Bootstrapper _bootstrapper;
        private readonly HostConfigurationProvider _hostConfigurationProvider;
        private readonly NancyBaseUriProvider _nancyBaseUriProvider;

        public NancyHostProvider(Bootstrapper bootstrapper, HostConfigurationProvider hostConfigurationProvider,
                                 NancyBaseUriProvider nancyBaseUriProvider)
        {
            _bootstrapper = bootstrapper;
            _hostConfigurationProvider = hostConfigurationProvider;
            _nancyBaseUriProvider = nancyBaseUriProvider;
        }

        private NancyHost _nancyHost;

        public NancyHost NancyHost
        {
            get
            {
                if (_nancyHost == null)
                {
                    _nancyHost = CreateNancyHost();
                }
                return _nancyHost;
            }
        }

        private NancyHost CreateNancyHost()
        {
            return new NancyHost(_bootstrapper, _hostConfigurationProvider.HostConfiguration, _nancyBaseUriProvider.Uri);
        }
    }
}
