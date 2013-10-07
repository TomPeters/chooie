using System;

namespace chui.Jobs
{
    public interface IJobFactory
    {
        IJob CreateJob(string name, Action action);
    }
}