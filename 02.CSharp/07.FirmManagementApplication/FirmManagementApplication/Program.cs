using AttendanceManagement;
using SalaryManagement;

namespace FirmManagementApplication
{
    internal class Program
    {
        /// <summary>
        /// Startup point of the application
        /// </summary>
        static void Main()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("\nRoles:");
                    Console.WriteLine("1. Administrator\n2. Employee");
                    Console.Write("Choose Role: ");
                    int RoleId;
                    bool isValidRoleId = int.TryParse(Console.ReadLine(), out RoleId);
                    if (!isValidRoleId)
                    {
                        Console.WriteLine("Enter valid role");
                        continue;
                    }
                    Role Role = (Role)RoleId;

                    switch (Role)
                    {
                        case Role.Administrator:
                            Adminstrator.Start();
                            break;
                        case Role.Employee:
                            Employee.Start();
                            break;
                        default:
                            Console.WriteLine("Enter a valid choice");
                            continue;
                    }

                    Console.WriteLine("Do you want to continue? yes or no");
                    string Continue = Console.ReadLine();
                    bool WantToContinue = Continue.ToLower() == "yes" ? true : false;
                    if (!WantToContinue)
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}