using System;
using System.Collections.Generic;

namespace chui.Jobs
{
    public interface IJobQueue
    {
        IEnumerable<IJob> Jobs { get; }
        void EnqueuJob(string name, Action action);
    }
}