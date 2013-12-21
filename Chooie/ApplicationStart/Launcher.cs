using System;
using Chooie.Interface.Logging;

namespace Chooie.ApplicationStart
{
    public static class Launcher
    {
        private const string Url = "http://localhost:9876/";
        private static readonly Uri Uri = new Uri(Url);

        public static void Main(string[] args)
        {
            var startupContainer = new StartupContainerFactory().CreateStartupContainer();
            try
            {
                startupContainer.Resolve<ApplicationStarter>().Start();
            }
            catch (Exception ex)
            {
                var logger = startupContainer.Resolve<ILogger>();
                logger.LogError(ex.Message);
                logger.LogError(ex.StackTrace);
            }
        }
    }
}
