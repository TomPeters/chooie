using System;

namespace Chooie.Jobs
{
    public interface IJobFactory
    {
        IJob CreateJob(string name, Action action);
        IJob CreateJobWithPostRunAction(string name, Action action, Action postRunAction);
    }
}