using System;
using System.Threading.Tasks;
using Chooie.Interface.Logging;

namespace Chooie.Jobs
{
    public class Job : IJob
    {
        private readonly string _name;
        private readonly Action _action;
        private readonly Action _postRunAction;
        private readonly IJobListUpdater _jobListUpdater;
        private readonly ILogger _logger;

        public Job(string name, Action action, IJobListUpdater jobListUpdater, ILogger logger)
            : this(name, action, () => { }, jobListUpdater, logger)
        {
        }

        public Job(string name, Action action, Action postRunAction, IJobListUpdater jobListUpdater, ILogger logger)
        {
            _name = name;
            _action = action;
            _postRunAction = postRunAction;
            _jobListUpdater = jobListUpdater;
            _logger = logger;
            State = JobState.Pending;
        }

        public async Task RunAsync()
        {
            try
            {
                _logger.LogInfo("Running Job: " + Name);
                State = JobState.Running;
                var task = new Task(_action);
                task.Start();
                await task;
                _postRunAction();
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
