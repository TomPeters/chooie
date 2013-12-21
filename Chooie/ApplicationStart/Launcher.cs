using System;
using Chooie.Interface.Logging;

namespace Chooie.ApplicationStart
{
    public static class Launcher
    {
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
