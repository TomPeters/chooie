using Nancy.Hosting.Self;

namespace Chooie.Nancy
{
    public class HostConfigurationProvider
    {
        public HostConfiguration HostConfiguration
        {
            get
            {
                var hostConfiguration = new HostConfiguration();
                hostConfiguration.UrlReservations.CreateAutomatically = true;
                return hostConfiguration;
            }
        }
    }
}
