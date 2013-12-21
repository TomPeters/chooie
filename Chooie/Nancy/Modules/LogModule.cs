using Chooie.Logging;
using Nancy;

namespace Chooie.Nancy.Modules
{
    public class LogModule : NancyModule
    {
        public LogModule(IMemoryLog log)
        {
            Get["/log"] = _ => Response.AsJson(log.Messages);
        }
    }
}
