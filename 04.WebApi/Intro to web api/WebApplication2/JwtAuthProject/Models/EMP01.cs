namespace JwtAuthProject.Models
{
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
        public EMP01(int p01f01, string p01f02)
        {
            this.p01f01 = p01f01;
            this.p01f02 = p01f02;
        }
    }
}