using StrBuilderImpl.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace StrBuilderImpl
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("Hello,");
            builder.Append("Good Morning");
            builder.Append(", Prajval");

            Console.WriteLine(builder.ToString());

            /*
            Person person = new Person();
            person.Name = "Prajval Gahine";
            person.Age = 22;
            person.Address = new Address()
            {
                PlotNo = 257,
                Street = "VTSRB",
                City = "Surat",
                State = "Gujarat"
            };

            // create a binary formatter
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("person.bin", FileMode.Create, FileAccess.Write);

            formatter.Serialize(stream, person);
            stream.Close();
            */

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("person.bin", FileMode.Open, FileAccess.Read);
            Person person2 = (Person)formatter.Deserialize(stream);
            stream.Close();

            Console.WriteLine(person2.ToString());

        }
    }
}