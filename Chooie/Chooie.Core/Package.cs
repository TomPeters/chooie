using System;

namespace Chooie.Core
{
    public class Package
    {
        public String Name { get; set; }
        public String CurrentVersion { get; set; }

        public new String ToString()
        {
            return Name;
        }
    }
}