using System;

namespace StaffDatabaseLib
{
    [PropType]
    public sealed class DepartmentChief : Employee
    {
        public DepartmentChief(NameType name,
                               DateTime birthDate,
                               SexType sex,
                               string department = "")
            : base(name, birthDate, sex)
        {
            Department = department;
        }

        public DepartmentChief( Employee employee,
                                string department = "")
            : base(employee.Name, employee.BirthDate, employee.Sex)
        {
            Department = department;
        }

        [Prop(Name = "Department: ", Priority = 4)]
        public string Department { set; get; }

        public override string ToString()
        {
            return base.ToString() + " " + Department;
        }
    }
}
