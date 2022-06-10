using StaffDatabaseLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StaffDatabaseApp
{
    public interface DatabaseIface
    {
        void Show();

        void Add();

        void Delete();

        void SaveToFile();

        void LoadFromFile();
    }

    public sealed class Input
    {
        public static NameType InputName()
        {
            Console.WriteLine("Type first name: ");
            string fstName = Console.ReadLine();

            Console.WriteLine("Type second name: ");
            string sndName = Console.ReadLine();

            Console.WriteLine("Type patronymic: ");
            string patronymic = Console.ReadLine();

            Console.WriteLine();

            return new NameType(fstName, sndName, patronymic);
        }        
        public static DateTime InputBirthDate()
        {
            Console.WriteLine("Type date of birth in format dd.mm.yyyy : ");
            string dateStr = Console.ReadLine();

            DateTime date;

            while (!DateTime.TryParse(dateStr, out date))
            {
                Console.Write("This is not valid input. Please enter a date of birth in format dd.mm.yyyy : \n");
                dateStr = Console.ReadLine();
            }

            Console.WriteLine();

            return date;
        }

        public static SexType InputSex()
        {
            Console.WriteLine("Type sex : ");
            Console.WriteLine("[0] male");
            Console.WriteLine("[1] female");

            string inputSexStr = Console.ReadLine();
            byte inputSexByte = 0;

            while (!byte.TryParse(inputSexStr, out inputSexByte) || inputSexByte >= 2 )
            {
                Console.Write("This is not valid input. Please enter a value from 0 to 1: \n");
                inputSexStr = Console.ReadLine();
            }

            Console.WriteLine();

            if (inputSexByte == 0)
            {
                return SexType.male;
            }


            return SexType.female;
        }
    }

    public sealed class Database : DatabaseIface
    {
        private StaffDatabase _staff = new StaffDatabase();

        private void AddKeyIfNotExists(EmployeeId id)
        {
            if (!_staff.ContainsKey(id))
            {
                HashSet<Employee> set = new HashSet<Employee>();
                _staff.Add(id, set);
            }
        }

        public void Add()
        {
            Console.WriteLine("Fill employee information.");

            NameType name = Input.InputName();

            DateTime date = Input.InputBirthDate();

            SexType sex = Input.InputSex();

            Console.WriteLine("Type employee job type: ");
            Console.WriteLine("[1] Director");
            Console.WriteLine("[2] Department Chief");
            Console.WriteLine("[3] Inspector");
            Console.WriteLine("[4] Worker");
            Console.WriteLine("[0] Exit");

            string jobStr = Console.ReadLine();
            byte jobByte;

            while (!byte.TryParse(jobStr, out jobByte) || jobByte >= 5)
            {
                Console.Write("This is not valid input. Please enter a value from 0 to 4: \n");
                jobStr = Console.ReadLine();
            }

            Console.WriteLine();

            if (jobByte == 0)
            {
                return;
            }

            EmployeeId id = new EmployeeId(name);

            AddKeyIfNotExists(id);

            switch (jobByte)
            {
                case 1:
                    Console.WriteLine("Type Area Of Directing: ");
                    string area = Console.ReadLine();

                    Employee employee_dir = new Director(name, date, sex, area);

                    _staff[id].Add(employee_dir);

                    break;
                case 2:
                    Console.WriteLine("Type Department: ");
                    string department = Console.ReadLine();

                    Employee employee_chief = new DepartmentChief(name, date, sex, department);

                    _staff[id].Add(employee_chief);

                    break;
                case 3:
                    Console.WriteLine("Type inspected object: ");
                    string inspectedObj = Console.ReadLine();

                    Employee employee_insp = new Inspector(name, date, sex, inspectedObj);

                    _staff[id].Add(employee_insp);

                    break;
                case 4:
                    NameType chiefName = Input.InputName();

                    Employee employee = new Worker(name, date, sex, chiefName);

                    _staff[id].Add(employee);

                    break;
            }

            Console.WriteLine();
        }

        public void Delete()
        {
            Console.WriteLine("Type employee's name to delete. ");

            NameType name = Input.InputName();

            EmployeeId id = new EmployeeId(name);

            List<Employee> collisions = new List<Employee>();

            HashSet<Employee> set;

            if (!_staff.TryGetValue(id, out set))
            {
                Console.WriteLine("No employees with such name '{0}'.\n", name.ToString());
                return;
            }

            foreach (Employee emp in set)
            {
                collisions.Add(emp);
            }

            byte idx = 0;

            foreach (var emp in collisions)
            {
                Console.Write("[{0}] ", idx);
                PropertyPage.ShowProperties(emp);
                idx++;
            }

            Console.WriteLine("Type index to delete: ");
            string idxStr = Console.ReadLine();

            while (!byte.TryParse(idxStr, out idx) || idx >= collisions.Count())
            {
                Console.Write("This is not valid input. Please enter a value from 0 to {0}.\n", collisions.Count());
                idxStr = Console.ReadLine();
            }

            Console.WriteLine();

            _staff[id].Remove(collisions[idx]);
        }

        public void Show()
        {
            Console.WriteLine("Choose job type: ");
            Console.WriteLine("[1] All ");
            Console.WriteLine("[2] Director");
            Console.WriteLine("[3] Department Chief");
            Console.WriteLine("[4] Inspector");
            Console.WriteLine("[5] Worker");
            Console.WriteLine("[0] Exit");

            string inputJobTypeStr = Console.ReadLine();
            byte inputJobTypeByte;

            while (!byte.TryParse(inputJobTypeStr, out inputJobTypeByte) || inputJobTypeByte >= 6)
            {
                Console.Write("This is not valid input. Please enter a value from 0 to 5: \n");
                inputJobTypeStr = Console.ReadLine();
            }

            Console.WriteLine();

            if (inputJobTypeByte == 0)
            {
                return;
            }

            Type type;

            switch (inputJobTypeByte)
            {
                case 2:
                    type = typeof(Director);
                    break;
                case 3:
                    type = typeof(DepartmentChief);
                    break;
                case 4:
                    type = typeof(Inspector);
                    break;
                case 5:
                    type = typeof(Worker);
                    break;
                default:
                    type = typeof(Employee);
                    break;
            }

            foreach (var pair in _staff)
            {
                foreach (Employee emp in pair.Value)
                {
                    if (type.Equals(typeof(Employee)) || emp.GetType().Equals(type))
                    {
                        Console.WriteLine("Job: " + emp.GetType().Name);
                        PropertyPage.ShowProperties(emp);
                        Console.WriteLine();
                    }
                }
            }
        }
        public void SaveToFile()
        {
            string path = "result.txt";

            using (StreamWriter writer = new StreamWriter(path, false))
            {
                string text = string.Empty;

                foreach (var pair in _staff)
                {
                    foreach (var emp in pair.Value)
                    {
                        text = emp.GetType().Name + " " + emp.ToString();
                        writer.WriteLine(text);
                    }
                }
            }
        }

        public void LoadFromFile()
        {
            string path = "result.txt";

            using (StreamReader reader = new StreamReader(path))
            {
                string line = string.Empty;

                while(line != null)
                {
                    line = reader.ReadLine();

                    if (line == null)
                    {
                        return;
                    }
                    string[] words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (words.Count() < 6)
                    {
                        Console.WriteLine("Error: Record {0} has not enough arguments.", line);
                        continue;
                    }

                    NameType name = new NameType(words[1], words[2], words[3]);

                    DateTime date;

                    if (!DateTime.TryParse(words[4], out date))
                    {
                        Console.WriteLine("Error: Record '{0}' has invalid date({1}).", line, words[4]);
                        continue;
                    }

                    SexType sex;

                    if (words[5].Equals("male"))
                    {
                        sex = SexType.male;
                    }
                    else if (words[5].Equals("female"))
                    {
                        sex = SexType.female;
                    }
                    else
                    {
                        Console.WriteLine("Error: Record '{0}' has invalid sex({1}).", line, words[5]);
                        continue;
                    }

                    EmployeeId id = new EmployeeId(name);

                    string param1 = string.Empty;
                    string param2 = string.Empty;
                    string param3 = string.Empty;

                    if (words.Count() > 6)
                    {
                        param1 = words[6];
                    }                    
                    if (words.Count() > 7)
                    {
                        param2 = words[7];
                    }                    
                    if (words.Count() > 8)
                    {
                        param3 = words[8];
                    }

                    AddKeyIfNotExists(id);
                    
                    switch (words[0])
                    {
                        case "Director":
                            Director dir = new Director(name, date, sex, param1);
                            _staff[id].Add(dir);
                            break;
                        case "DepartmentChief":
                            DepartmentChief chief = new DepartmentChief(name, date, sex, param1);
                            _staff[id].Add(chief);
                            break;
                        case "Inspector":
                            Inspector inspector = new Inspector(name, date, sex, param1);
                            _staff[id].Add(inspector);
                            break;
                        case "Worker":
                            NameType chiefName = new NameType(param1, param2, param3);
                            Worker worker = new Worker(name, date, sex, chiefName);
                            _staff[id].Add(worker);
                            break;
                        default:
                            Console.WriteLine("Error: Record {0} has invalid job specifier({1}).", line, words[0]);
                            break;
                    }
                }
            }
        }
    }
}
