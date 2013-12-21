using Chooie.ApplicationStart;
using Chooie.Interface.Logging;
using Nancy.Hosting.Self;

namespace Chooie.Nancy
{
    public class NancyStarter : IStarter
    {
        private readonly NancyHost _nancyHost;
        private readonly ILogger _logger;

        public NancyStarter(NancyHost nancyHost, ILogger logger)
        {
            _nancyHost = nancyHost;
            _logger = logger;
        }

        public void Start()
        {
            _logger.LogInfo("Starting web server");
            _nancyHost.Start();
        }
    }
}
