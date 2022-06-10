using System;

namespace StaffDatabaseLib
{
    [PropType]
    public sealed class Inspector : Employee
    {
        public Inspector(NameType name,
                        DateTime birthDate,
                        SexType sex,
                        string inspectedObject = "")
            : base(name, birthDate, sex)
        {
            InspectedObject = inspectedObject;
        }        
        
        public Inspector(Employee employee,   
                        string inspectedObject = "")
            : base(employee.Name, employee.BirthDate, employee.Sex)
        {
            InspectedObject = inspectedObject;
        }

        [Prop(Name = "Inspected Object: ", Priority = 4)]
        public string InspectedObject { set; get; }

        public override string ToString()
        {
            return base.ToString() + " " + InspectedObject;
        }
    }
}
