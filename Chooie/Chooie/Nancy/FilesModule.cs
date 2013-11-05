using Nancy;

namespace Chooie.Nancy
{
    public class FilesModule : NancyModule
    {
        public FilesModule()
        {
            Get["/"] = _ => Response.AsFile("static/index.html");
        }
    }
}
