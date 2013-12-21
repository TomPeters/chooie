using System.Windows.Forms;
using Chooie.ApplicationStart;
using Chooie.Interface.Logging;
using Chooie.Nancy;

namespace Chooie.SystemTrayApplication
{
    public class SystemTrayApplicationStarter : IStarter
    {
        private readonly NancyHostProvider _nancyHostProvider;
        private readonly NancyBaseUriProvider _nancyBaseUriProvider;
        private readonly ILogger _logger;

        public SystemTrayApplicationStarter(NancyHostProvider nancyHostProvider, NancyBaseUriProvider nancyBaseUriProvider, ILogger logger)
        {
            _nancyHostProvider = nancyHostProvider;
            _nancyBaseUriProvider = nancyBaseUriProvider;
            _logger = logger;
        }

        public void Start()
        {
            _logger.LogInfo("Configuring system tray icon");
            var applicationContext = new SystemTrayApplicationContext(_nancyHostProvider.NancyHost, _nancyBaseUriProvider.Url);
            applicationContext.Initialize();

            _logger.LogInfo("Starting chooie (entering main loop)");
            Application.Run(applicationContext);
        }
    }
}
