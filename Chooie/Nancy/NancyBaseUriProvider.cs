using System;

namespace Chooie.Nancy
{
    public class NancyBaseUriProvider
    {
        public Uri Uri
        {
            get
            {
                return new Uri(Url);
            }
        }

        public string Url
        {
            get
            {
                return "http://localhost:9876/";
            }
        }
    }
}
