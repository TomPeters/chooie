using Nancy;
using chui.Jobs;

namespace chui.Nancy
{
    public class JobsModule : NancyModule
    {
        public JobsModule(IJobQueue jobQueue)
        {
            Get["/jobs"] = _ => Response.AsJson(jobQueue.Jobs);
        }
    }
}
