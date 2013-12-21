using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Chooie.Interface.Logging;

namespace Chooie.Jobs
{
    public class JobQueue : IJobQueue
    {
        private readonly ILogger _logger;

        public JobQueue(ILogger logger)
        {
            _logger = logger;
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

        public void EnqueueJob(IJob job)
        {
            _pendingJobs.Enqueue(job);
            _logger.LogInfo("Job added: " + job.Name);
            RunJobsIfNotExecuting();
        }

        private void RunJobsIfNotExecuting()
        {
            if (_runningJob == null)
            {
                RunJobsAsync();
            }
        }

        private async Task RunJobsAsync()
        { 
            if (!_pendingJobs.Any()) return;

            _runningJob = _pendingJobs.Dequeue();
            await _runningJob.RunAsync();
            _completedJobs.Add(_runningJob);
            _runningJob = null;
            await RunJobsAsync();
        }
    }
}
