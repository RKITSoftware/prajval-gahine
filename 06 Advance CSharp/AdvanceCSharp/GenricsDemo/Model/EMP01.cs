
namespace GenricsDemo.Model
{
    internal class EMP01 : IResource
    {

        /// <summary>
        /// Employee first name
        /// </summary>
        public string p01f02 { get; set; }

        /// <summary>
        /// Employee last name
        /// </summary>
        public string p01f03 { get; set; }

        /// <summary>
        /// Employee gender
        /// </summary>
        public string p01f04 { get; set; }

        /// <summary>
        /// Employee Date of birth
        /// </summary>
        public DateTime p01f05 { get; set; }

        public EMP01(int id, string firstName, string lastName, string gender, DateTime DOB)
        {
            this.id = id;
            this.p01f02 = firstName;
            this.p01f03 = lastName;
            this.p01f04 = gender;
            this.p01f05 = DOB;
        }
    }
}
