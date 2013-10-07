using System;

namespace chui.Jobs
{
    public class JobFactory : IJobFactory
    {
        private readonly IJobListUpdater _jobListUpdater;

        public JobFactory(IJobListUpdater jobListUpdater)
        {
            _jobListUpdater = jobListUpdater;
        }

        public IJob CreateJob(string name, Action action)
        {
            return new Job(name, action, _jobListUpdater);
        }
    }
}
