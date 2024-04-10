using System.Reflection;
using System.Text;

namespace BaseClassLibraryDemo.Models
{
    /// <summary>
    /// A model class to model Person
    /// </summary>
    internal class PSN01
    {
        /// <summary>
        /// Person Id
        /// </summary>
        public int n01f01 { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string? n01f02 { get; set; }

        /// <summary>
        /// Age
        /// </summary>
        public int n01f03 { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Type type = this.GetType();
            PropertyInfo[] props = type.GetProperties();
            PropertyInfo last = props.Last();
            foreach (PropertyInfo prop in props)
            {
                sb.Append($"{prop.Name}: {prop.GetValue(this, null)}");
                if (!prop.Equals(last))
                {
                    sb.Append(",\n");
                }
                else
                {
                    sb.Append("\n");
                }
            }
            return sb.ToString();
        }
    }
}
