using System.Threading.Tasks;

namespace Chooie.Jobs
{
    public interface IJob
    {
        Task RunAsync();
        string Name { get; }
        JobState State { get; }
    }
}