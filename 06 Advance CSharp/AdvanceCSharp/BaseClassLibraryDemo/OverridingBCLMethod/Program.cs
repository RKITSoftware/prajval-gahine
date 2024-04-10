
using BaseClassLibraryDemo.Models;

namespace BaseClassLibraryDemo.OverridingBCLMethod
{
    /// <summary>
    /// Program class for entry point for Custom class ToString overriding Demo
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Method for entry point for Custom class ToString overriding Demo
        /// </summary>
        public static void Main()
        {
            List<PSN01> lstPerson = new List<PSN01>()
            {
                new PSN01{ n01f01 = 1, n01f02 = "Deep Patel", n01f03 = 21 },
                new PSN01{ n01f01 = 2, n01f02 = "Krinsi Kayada", n01f03 = 21 },
                new PSN01{ n01f01 = 3, n01f02 = "Prajval Gahine", n01f03 = 22 },
                new PSN01{ n01f01 = 4, n01f02 = "Yash Lathiya", n01f03 = 22 }
            };

            foreach (PSN01 person in lstPerson)
            {
                string describe = person.ToString();
                Console.WriteLine(describe);
            }

            PSN01 p1 = new PSN01 { n01f01 = 1, n01f02 = "Deep Patel", n01f03 = 21 };
            Console.WriteLine(p1.ToString());
        }
    }
}
