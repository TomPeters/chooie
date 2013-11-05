using System;

namespace Chooie.Jobs
{
    public interface IJobFactory
    {
        IJob CreateJob(string name, Action action);
    }
}