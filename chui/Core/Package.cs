using System;

namespace Core
{
    public class Package
    {
        public String Name { get; set; }
        public String InstalledVersion { get; set; }
        public Boolean IsPreRelease { get; set; }

        public new String ToString()
        {
            return Name;
        }
    }
}