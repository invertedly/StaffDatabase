using System;

namespace StaffDatabaseLib
{
    public sealed class EmployeeId
    {
        public NameType Name { get; set; }

        public EmployeeId(NameType name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            EmployeeId x = this;
            EmployeeId y = (EmployeeId)obj;
            string xNameStr = x.Name.ToString();
            string yNameStr = y.Name.ToString();
            return xNameStr.Equals(yNameStr);
        }

        public override int GetHashCode()
        {
            return Name.ToString().GetHashCode();
        }
    }
}
