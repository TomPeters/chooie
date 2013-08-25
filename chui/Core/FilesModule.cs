using Nancy;

namespace Core
{
    public class FilesModule : NancyModule
    {
        public FilesModule()
        {
            Get["/"] = parameters => "test";
        }
    }
}
