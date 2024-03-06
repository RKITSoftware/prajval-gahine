namespace FirmWebApiDemo.Models
{
    /// <summary>
    /// User Employee combiner model
    /// </summary>
    public class USREMP
    {
        public USR01 user { get; set; }

        public EMP01 employee { get; set; }
    }
}