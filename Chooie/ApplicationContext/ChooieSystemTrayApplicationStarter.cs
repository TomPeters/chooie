using System.Windows.Forms;
using Chooie.ApplicationStart;
using Chooie.Interface.Logging;
using Chooie.Nancy;
using Nancy.Hosting.Self;

namespace Chooie.ApplicationContext
{
    public class ChooieSystemTrayApplicationStarter : IStarter
    {
        private readonly NancyHost _nancyHost;
        private readonly NancyBaseUriProvider _nancyBaseUriProvider;
        private readonly ILogger _logger;

        public ChooieSystemTrayApplicationStarter(NancyHost nancyHost, NancyBaseUriProvider nancyBaseUriProvider, ILogger logger)
        {
            _nancyHost = nancyHost;
            _nancyBaseUriProvider = nancyBaseUriProvider;
            _logger = logger;
        }

        public void Start()
        {
            _logger.LogInfo("Configuring system tray icon");
            var applicationContext = new ChooieApplicationContext(_nancyHost, _nancyBaseUriProvider.Url);
            applicationContext.Initialize();

            _logger.LogInfo("Starting chooie (entering main loop)");
            Application.Run(applicationContext);
        }
    }
}
