using Nancy;

namespace Chooie.Nancy.Modules
{
    public class FilesModule : NancyModule
    {
        public FilesModule()
        {
            Get["/"] = _ => Response.AsFile("static/index.html");
        }
    }
}
