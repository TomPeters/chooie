using Chooie.ApplicationStart;
using Chooie.Interface.Logging;
using Microsoft.Owin.Hosting;

namespace Chooie.SignalR
{
    public class SignalRStarter : IStarter
    {
        private readonly SignalRUriProvider _uriProvider;
        private readonly ILogger _logger;

        public SignalRStarter(SignalRUriProvider uriProvider, ILogger logger)
        {
            _uriProvider = uriProvider;
            _logger = logger;
        }

        public void Start()
        {
            _logger.LogInfo("Attempting to setup acl for urls for signalR");
            new UrlAclConfigurer().ConfigureUrl(_uriProvider.Uri);
            _logger.LogInfo("Starting signal R at " + _uriProvider.Uri);
            WebApp.Start<SignalRStartup>(_uriProvider.Uri);
        }
    }
}
