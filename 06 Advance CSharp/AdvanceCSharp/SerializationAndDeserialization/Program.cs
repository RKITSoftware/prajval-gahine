using Newtonsoft.Json;
using SerializationAndDeserialization.Model;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace SerializationAndDeserialization
{
    /// <summary>
    /// Program class for entry of Serialization-Deserialization demo
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// A httpclient
        /// </summary>
        static readonly HttpClient client = new HttpClient();

        /// <summary>
        /// Main method - entry point for sealed class demo
        /// </summary>
        [STAThread]
        static async Task Main(string[] args)
        {
            //System.Threading.Thread.CurrentThread.SetApartmentState(System.Threading.ApartmentState.STA);

            string urlPath = "https://jsonplaceholder.typicode.com/users/1";
            // json deserialize
            User user = await GetUser(urlPath);
            Console.WriteLine("\n\nuser1 (deserialization using json)");
            DisplayUser(user, 0);

            // json serialization
            //string jsonString = JsonSerializer.Serialize(user);
            string jsonString = JsonConvert.SerializeObject(user);
            HttpResponseMessage responseMessage = await client.SendAsync(new HttpRequestMessage(HttpMethod.Post, "https://jsonplaceholder.typicode.com/users"));
            Console.WriteLine(responseMessage.IsSuccessStatusCode ? "user created" : "unable to create user");


            // xml serialization
            // create xml object for specific .net type
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(User));

            FileStream fs = new FileStream("./user.xml", FileMode.Create);

            xmlSerializer.Serialize(fs, user);
            Console.WriteLine();

            // xml deserialization
            fs.Position = 0;
            User user2 = (User)xmlSerializer.Deserialize(fs);
            Console.WriteLine("\n\nuser2 (deserialization using xml)");
            DisplayUser(user2, 0);


            // binary serialization

            //1. create a sample .net object => here we have user
            //2. create MemoryStream of user object
            MemoryStream ms = new MemoryStream();

            // 3. create a binary formatter instance
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, user);

            ms.Position = 0;


            //Clipboard.SetText(Encoding.UTF8.GetString(ms.ToArray()));
            User user3 = (User)bf.Deserialize(ms);
            Console.WriteLine("\n\nuser3 (deserialization using binary)");
            DisplayUser(user3, 0);
        }

        /// <summary>
        /// Method to get an user
        /// </summary>
        /// <param name="urlPath">url path for an user</param>
        /// <returns>Task<User> instance</returns>
        public static async Task<User> GetUser(string urlPath)
        {
            using HttpResponseMessage response = await client.GetAsync(urlPath);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            // deserialize the json string to User .net object
            User user = JsonConvert.DeserializeObject<User>(responseBody);
            //User user = JsonSerializer.Deserialize<User>(responseBody);
            return user;
        }

        /// <summary>
        /// Method to display an user detail in formatted manner
        /// </summary>
        /// <param name="user">User object</param>
        /// <param name="level">current intendation level</param>
        public static void DisplayUser(Object? user, int level)
        {
            if (user == null)
            {
                return;
            }
            if (user is string || user is int)
            {
                Console.WriteLine(user);
                return;
            }
            foreach (PropertyInfo prop in user.GetType().GetProperties())
            {
                Console.Write(new string('\t', level) + prop.Name + ": ");
                object? propValue = prop.GetValue(user, null);
                if (!(propValue is string || propValue is int))
                {
                    Console.WriteLine();
                }
                DisplayUser(propValue, level + 1);
            }
        }
    }
}