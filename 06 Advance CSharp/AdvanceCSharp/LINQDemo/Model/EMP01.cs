namespace LINQDemo.Model
{
    /// <summary>
    /// Model class for Employee
    /// </summary>
    internal class EMP01
    {
        /// <summary>
        /// Employee Id
        /// </summary>
        public int p01f01 { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string p01f02 { get; set; }

        /// <summary>
        /// Date of Joining
        /// </summary>
        public DateOnly p01f03 { get; set; }

        /// <summary>
        /// Constructor to create Employee instance with name and date of joining
        /// </summary>
        /// <param name="name">Employee name</param>
        /// <param name="doj">Date of Joining</param>
        public EMP01(string name, DateOnly doj) : this(-1, name, doj)
        {
            this.p01f01 = -1;
            this.p01f02 = name;
            this.p01f03 = doj;
        }

        /// <summary>
        /// Constructor to create Employee Instacne with Employee id, Employee name and date of joining
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <param name="name">Employee Name</param>
        /// <param name="date_of_joining">Date of Joining</param>
        public EMP01(int id, string name, DateOnly date_of_joining)
        {
            this.p01f01 = id;
            this.p01f02 = name;
            this.p01f03 = date_of_joining;
        }
    }
}
