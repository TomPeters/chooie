using Chooie.SignalR;

namespace Chooie.Jobs
{
    public class JobListUpdater : IJobListUpdater
    {
        private const string DispatchId = "JobList";

        private readonly IClientMessenger _clientMessenger;

        public JobListUpdater(IClientMessenger clientMessenger)
        {
            _clientMessenger = clientMessenger;
        }

        public void UpdateJobList()
        {
            _clientMessenger.SendMessage(DispatchId, "Changed");
        }
    }
}
