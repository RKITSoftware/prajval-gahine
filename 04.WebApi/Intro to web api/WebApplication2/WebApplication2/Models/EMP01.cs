namespace WebApplication2.Models
{
    /// <summary>
    /// Employee class to store employee specific data
    /// </summary>
    public class EMP01
    {
        /// <summary>
        /// Employee id
        /// </summary>
        public int p01f01 { get; set; }

        /// <summary>
        /// Employee name
        /// </summary>
        public string p01f02 { get; set; }

        /// <summary>
        /// Constructor to create EMP01 instance with employee id and name
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <param name="name">Employee name</param>
        public EMP01(int id, string name)
        {
            this.p01f01 = id;
            this.p01f02 = name;
        }
    }
}