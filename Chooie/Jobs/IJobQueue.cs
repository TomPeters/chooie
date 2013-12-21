using System.Collections.Generic;

namespace Chooie.Jobs
{
    public interface IJobQueue
    {
        IEnumerable<IJob> Jobs { get; }
        void EnqueueJob(IJob job);
    }
}