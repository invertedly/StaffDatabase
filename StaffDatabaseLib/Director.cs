using System;

namespace StaffDatabaseLib
{
    [PropType]
    public sealed class Director : Employee
    {
        public Director(NameType name,
                        DateTime birthDate,
                        SexType sex,
                        string areaOfDirecting = "")
            : base(name, birthDate, sex)
        {
            AreaOfDirecting = areaOfDirecting;
        }

        public Director(Employee employee,
                        string areaOfDirecting = "")
            : base(employee.Name, employee.BirthDate, employee.Sex)
        {
            AreaOfDirecting = areaOfDirecting;
        }

        [Prop(Name = "Area Of Directing: ", Priority = 4)]
        public string AreaOfDirecting { set; get; }

        public override string ToString()
        {
            return base.ToString() + " " + AreaOfDirecting;
        }
    }
}
