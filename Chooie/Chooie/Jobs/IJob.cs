namespace Chooie.Jobs
{
    public interface IJob
    {
        void Run();
        string Name { get; }
        JobState State { get; }
    }
}