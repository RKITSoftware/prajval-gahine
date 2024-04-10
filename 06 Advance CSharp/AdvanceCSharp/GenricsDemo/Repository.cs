using GenricsDemo.Model;
using Newtonsoft.Json;

namespace GenricsDemo
{
    /// <summary>
    /// Generic Repository class
    /// </summary>
    /// <typeparam name="T">A Resource</typeparam>
    internal class Repository<T> where T : IResource
    {
        /// <summary>
        /// File path to a .../[resource].json
        /// </summary>
        private static string? filePath;

        /// <summary>
        /// Generic Repository class constructor
        /// </summary>
        /// <param name="filePath"></param>
        public Repository(string filePath)
        {
            Repository<T>.filePath = filePath;
        }

        /// <summary>
        /// Gets an resource of type T based on resource id
        /// </summary>
        /// <param name="id">resource id</param>
        /// <returns>a resouce of type T</returns>
        public T? GetById(int id)
        {
            List<T>? lstResource = null;
            using (StreamReader sr = new StreamReader(filePath))
            {
                string strResources = sr.ReadToEnd();
                lstResource = JsonConvert.DeserializeObject<List<T>>(strResources);
            }
            if (lstResource != null)
            {
                T? resource = lstResource.FirstOrDefault<T>(r => r.id == id);
                return resource;
            }
            return null;
        }

        /// <summary>
        /// Save method to save an resource to .../[resource].json file
        /// </summary>
        /// <param name="resource">IResource object</param>
        /// <returns></returns>
        public bool Save(T resource)
        {
            // get resource list
            List<T>? lstResource = null;
            using (StreamReader sr = new StreamReader(filePath))
            {
                string strResources = sr.ReadToEnd();
                try
                {
                    lstResource = JsonConvert.DeserializeObject<List<T>>(strResources);
                }
                catch (Exception)
                {
                    return false;
                }
            }
            if (lstResource != null)
            {
                lstResource.Add(resource);
            }
            else
            {
                lstResource = new List<T>(1) { resource };
            }

            // now serialize the .net List<T>
            string jsonLstResource = JsonConvert.SerializeObject(lstResource, Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.WriteLine(jsonLstResource);
            }
            return true;
        }
    }
}
