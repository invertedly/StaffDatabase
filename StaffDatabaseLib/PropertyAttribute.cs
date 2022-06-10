using System;

namespace StaffDatabaseLib
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PropAttribute : Attribute
    {
        public PropAttribute()
        {
            Name = string.Empty;
            Priority = 1;
        }

        public string Name { get; set; }
        public int Priority { get; set; }
    }
}
