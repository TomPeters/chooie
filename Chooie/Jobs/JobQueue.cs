using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Chooie.Jobs
{
    public class JobQueue : IJobQueue
    {
        private readonly IJobFactory _jobFactory;

        public JobQueue(IJobFactory jobFactory)
        {
            _jobFactory = jobFactory;
        }

        private readonly Queue<IJob> _pendingJobs = new Queue<IJob>();
        private readonly IList<IJob> _completedJobs = new List<IJob>();
        private IJob _runningJob;

        public IEnumerable<IJob> Jobs
        {
            get
            {
                IList<IJob> jobs = _completedJobs.ToList();
                if (_runningJob != null)
                {
                    jobs.Add(_runningJob);
                }
                return jobs.Concat(_pendingJobs);
            }
        }
        
        public void EnqueueJob(string name, Action action)
        {
            _pendingJobs.Enqueue(_jobFactory.CreateJob(name, action));
            RunJobsIfNotExecuting();
        }

        private void RunJobsIfNotExecuting()
        {
            if (_runningJob == null)
            {
                new Thread(RunJobs).Start();
            }
        }

        private void RunJobs()
        { 
            if (!_pendingJobs.Any()) return;

            _runningJob = _pendingJobs.Dequeue();
            _runningJob.Run();
            _completedJobs.Add(_runningJob);
            _runningJob = null;
            RunJobs();
        }
    }
}
