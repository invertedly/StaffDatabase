using System;

namespace StaffDatabaseLib
{
    [PropType]
    public sealed class Worker : Employee
    {
        public Worker(NameType name,
                      DateTime birthDate,
                      SexType sex,
                      NameType chiefName)
            : base(name, birthDate, sex)
        {
            ChiefName = chiefName;
        }

        public Worker(Employee employee,
                      NameType chiefName)
            : base(employee.Name, employee.BirthDate, employee.Sex)
        {
            ChiefName = chiefName;
        }

        [Prop(Name = "ChiefName: ", Priority = 4)]
        public NameType ChiefName { set; get; }

        public override string ToString()
        {
            return base.ToString() + " " + ChiefName;
        }
    }
}
