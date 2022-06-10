using System;

namespace StaffDatabaseLib
{
    public sealed class BirthDateType
    {
        public BirthDateType(DateTime birthDate)
        {
            BirthDate = new DateTime(birthDate.Year, birthDate.Month, birthDate.Day);
        }

        public DateTime BirthDate { set; get; }

        public override string ToString()
        {
            return BirthDate.ToShortDateString();
        }
    }
}
