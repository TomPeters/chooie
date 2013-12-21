using System;
using Chooie.ApplicationContext;
using Chooie.Database;
using Chooie.Nancy;
using Chooie.SignalR;
using Nancy.Hosting.Self;

namespace Chooie.ApplicationStart
{
    public class Launcher
    {
        private const string Url = "http://localhost:9876/";
        private static readonly Uri Uri = new Uri(Url);

        public static void Main(string[] args)
        {
            var dependencyContainerBuilder = new NancyDependencyContainerBuilder();
            try
            {
                var logger = dependencyContainerBuilder.Logger;
                new SignalRStarter(new SignalRUriProvider(), logger).Start();
                new DatabaseStarter(new DatabaseAccessor(logger)).Start();

                logger.LogInfo("Configuring web server");
                var hostConfiguration = new HostConfiguration();
                hostConfiguration.UrlReservations.CreateAutomatically = true;
                var nancyHost = new NancyHost(new ChooieBootstrapper(dependencyContainerBuilder), hostConfiguration, Uri);

                new NancyStarter(nancyHost, logger).Start();

                new ChooieSystemTrayApplicationStarter(nancyHost, new NancyBaseUriProvider(), logger).Start();
            }

            catch (Exception ex)
            {
                dependencyContainerBuilder.Logger.LogError(ex.Message);
                dependencyContainerBuilder.Logger.LogError(ex.StackTrace);
            }
        }
    }
}
