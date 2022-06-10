using System;
using System.Collections.Generic;

namespace StaffDatabaseLib
{
    [PropType]
    public abstract class Employee
    {
        public Employee(NameType name,
                        DateTime birthDate,
                        SexType sex)
        {
            Name = name;
            BirthDate = birthDate;
            Sex = sex;
        }

        [Prop(Name = "Name: ", Priority = 1)]
        public NameType Name { set; get; }

        [Prop(Name = "BirthDate: ", Priority = 2)]
        public DateTime BirthDate { set; get; }

        [Prop(Name = "Sex: ", Priority = 3)]
        public SexType Sex { set; get; }

        public override string ToString()
        {
            return Name.ToString() + " " + BirthDate.ToShortDateString() + " " + Sex.ToString();
        }
    }
}
