using ServiceStack;
using ServiceStack.DataAnnotations;

namespace FirmAdvanceDemo.Models
{
    public class DPT01 : IModel
    {

        public static string TableName;
        static DPT01()
        {
            TableName = "Department";
        }

        /// <summary>
        /// Department Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        [Alias("t01f01")]
        public int Id { get; set; }

        /// <summary>
        /// Department Name
        /// </summary>
        [ValidateNotNull]
        public string t01f02 { get; set; }
    }
}