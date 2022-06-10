using System;

namespace StaffDatabaseApp
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;

            Console.WriteLine("Console Staff Database\r");
            Console.WriteLine("----------------------\n");

            Database staff = new Database();

            while (!endApp)
            {
                string inputStr;
                byte inputByte = 0;

                Console.WriteLine("Type operation number, and then press Enter: ");
                Console.WriteLine("[1] Show database");
                Console.WriteLine("[2] Add employee");
                Console.WriteLine("[3] Delete employee data");
                Console.WriteLine("[4] Save to file");
                Console.WriteLine("[5] Load from file");
                Console.WriteLine("[0] Exit");

                inputStr = Console.ReadLine();

                while (!byte.TryParse(inputStr, out inputByte) || inputByte >= 6)
                {
                    Console.Write("This is not valid input. Please enter a value from 0 to 5: \n");
                    inputStr = Console.ReadLine();
                }

                Console.WriteLine();

                switch (inputByte)
                {
                    case 1:
                        staff.Show();
                        break;
                    case 2:
                        staff.Add();
                        break;
                    case 3:
                        staff.Delete();
                        break;
                    case 4:
                        staff.SaveToFile();
                        break;
                    case 5:
                        staff.LoadFromFile();
                        break;
                    default:
                        break;
                }

                Console.WriteLine("------------------------\n");

                
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: \n");
                if (Console.ReadLine() == "n") endApp = true;
                
                Console.WriteLine();
            }
            return;
        }
    }
}
