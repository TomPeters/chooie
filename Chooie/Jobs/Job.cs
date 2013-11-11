using System;
using Chooie.Core.Logging;

namespace Chooie.Jobs
{
    public class Job : IJob
    {
        private readonly string _name;
        private readonly Action _action;
        private readonly IJobListUpdater _jobListUpdater;
        private readonly ILogger _logger;

        public Job(string name, Action action, IJobListUpdater jobListUpdater, ILogger logger)
        {
            _name = name;
            _action = action;
            _jobListUpdater = jobListUpdater;
            _logger = logger;
            State = JobState.Pending;
        }

        public void Run()
        {
            try
            {
                _logger.LogInfo("Running Job: " + Name);
                State = JobState.Running;
                _action();
                State = JobState.Finished;
                _logger.LogInfo("Finished Job: " + Name);
            }
            catch (Exception)
            {
                State = JobState.Error;
                _logger.LogError("Job Failed: " + Name);
            }
        }

        public string Name
        {
            get { return _name; }
        }

        private JobState _state;
        public JobState State 
        { 
            get { return _state; }
            private set
            {
                _state = value;
                _jobListUpdater.UpdateJobList();
            }
        }
    }
}
