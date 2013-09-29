using System.Collections.Generic;
using Nancy;

namespace Core.Nancy
{
    public class PackagesModule : NancyModule
    {
        public PackagesModule()
        {
            Get["/packages"] = _ =>
                {
                    var packages = new List<Package>
                        {
                            new Package {Name = "server package 1", Id = "ID1"},
                            new Package {Name = "server package 2", Id = "ID2"}
                        };
                    return Response.AsJson(packages);
                };
        }
    }
}
