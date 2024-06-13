namespace DgServer.Models.Domain
{
    public class Address
    {
        public int PlotNo { get; set; }

        public string? Street { get; set; }

        public string? Town { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Pincode { get; set; }
    }
}
