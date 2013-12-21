using Chooie.ApplicationStart;
using Chooie.Interface.Logging;

namespace Chooie.Nancy
{
    public class NancyStarter : IStarter
    {
        private readonly NancyHostProvider _nancyHostProvider;
        private readonly ILogger _logger;

        public NancyStarter(NancyHostProvider nancyHostProvider, ILogger logger)
        {
            _nancyHostProvider = nancyHostProvider;
            _logger = logger;
        }

        public void Start()
        {
            _logger.LogInfo("Starting web server");
            _nancyHostProvider.NancyHost.Start();
        }
    }
}
