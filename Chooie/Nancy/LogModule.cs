using Chooie.Logging;
using Nancy;

namespace Chooie.Nancy
{
    public class LogModule : NancyModule
    {
        public LogModule(IMemoryLog log)
        {
            Get["/log"] = _ => Response.AsJson(log.Messages);
        }
    }
}
