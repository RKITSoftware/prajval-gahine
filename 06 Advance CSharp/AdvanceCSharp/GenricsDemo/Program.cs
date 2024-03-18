using GenricsDemo.Model;

namespace GenricsDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Get a user's username
            Repository<USR01> userRepository = new Repository<USR01>("./data/User.json");
            USR01? user = userRepository.GetById(1);
            Console.WriteLine(user?.r01f02);

            // Get an employee name
            Repository<EMP01> employeeRepository = new Repository<EMP01>("./data/Employee.json");
            EMP01? employee = employeeRepository.GetById(1);
            Console.WriteLine(employee?.p01f02);

            // save a user data
            USR01 newUser = new USR01(4, "prajvalgahine", "Prajval@123", "employee");
            userRepository.Save(newUser);

            // save a user data
            EMP01 newEmployee = new EMP01(3, "Prajval", "Gahine", "male", DateTime.Parse("2001-12-07"));
            employeeRepository.Save(newEmployee);
        }
    }
}