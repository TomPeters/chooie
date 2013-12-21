namespace Chooie.ApplicationStart
{
    public class ApplicationStarter : IStarter
    {
        private readonly ApplicationStarterProvider _applicationStarterProvider;

        public ApplicationStarter(ApplicationStarterProvider applicationStarterProvider)
        {
            _applicationStarterProvider = applicationStarterProvider;
        }

        public void Start()
        {
            foreach (var starter in _applicationStarterProvider.Starters)
            {
                starter.Start();
            }
        }
    }
}
