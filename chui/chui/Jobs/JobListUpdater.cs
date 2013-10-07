using chui.SignalR;

namespace chui.Jobs
{
    public class JobListUpdater : IJobListUpdater
    {
        private const string DispatchId = "Job List";

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
