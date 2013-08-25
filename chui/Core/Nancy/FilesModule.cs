using Nancy;

namespace Core.Nancy
{
    public class FilesModule : NancyModule
    {
        public FilesModule()
        {
            Get["/"] = _ => Response.AsFile("static/index.html");
        }
    }
}
