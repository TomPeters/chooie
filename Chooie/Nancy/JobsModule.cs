using Nancy;
using Chooie.Jobs;

namespace Chooie.Nancy
{
    public class JobsModule : NancyModule
    {
        public JobsModule(IJobQueue jobQueue)
        {
            Get["/jobs"] = _ => Response.AsJson(jobQueue.Jobs);
        }
    }
}
