using System;

namespace Chooie.Jobs
{
    public class Job : IJob
    {
        private readonly string _name;
        private readonly Action _action;
        private readonly IJobListUpdater _jobListUpdater;

        public Job(string name, Action action, IJobListUpdater jobListUpdater)
        {
            _name = name;
            _action = action;
            _jobListUpdater = jobListUpdater;
            State = JobState.Pending;
        }

        public void Run()
        {
            try
            {
                State = JobState.Running;
                _action();
                State = JobState.Finished;
            }
            catch (Exception)
            {
                State = JobState.Error;
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
