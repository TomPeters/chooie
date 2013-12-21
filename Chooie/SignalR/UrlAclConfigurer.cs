using System.Security.Principal;
using Nancy.Hosting.Self;

namespace Chooie.SignalR
{
    public class UrlAclConfigurer
    {
        public void ConfigureUrl(string url)
        {
            string user = WindowsIdentity.GetCurrent().Name;
            NetSh.AddUrlAcl(url, user);
        }
    }
}
