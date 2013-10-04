using Nancy;

namespace chui.Nancy
{
    public class FilesModule : NancyModule
    {
        public FilesModule()
        {
            Get["/"] = _ => Response.AsFile("static/index.html");
        }
    }
}
