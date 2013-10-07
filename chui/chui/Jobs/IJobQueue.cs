using System;
using System.Collections.Generic;

namespace chui.Jobs
{
    public interface IJobQueue
    {
        IEnumerable<IJob> Jobs { get; }
        void EnqueueJob(string name, Action action);
    }
}