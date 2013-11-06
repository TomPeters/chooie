namespace Chooie.Jobs
{
    public interface IJobListUpdater
    {
        // Sends message to client to inform it that the job list has updated
        void UpdateJobList();
    }
}