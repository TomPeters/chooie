using System.Security.Principal;
using Nancy.Hosting.Self;

namespace chui.SignalR
{
    internal class UrlAclConfigurer
    {
        public void ConfigureUrl(string url)
        {
            string user = WindowsIdentity.GetCurrent().Name;
            NetSh.AddUrlAcl(url, user);
        }
    }
}
