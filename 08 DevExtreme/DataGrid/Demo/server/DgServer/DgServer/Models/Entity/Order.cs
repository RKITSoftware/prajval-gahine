using DgServer.Models.Domain;

namespace DgServer.Models.Entity
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public List<int> LstProductId { get; set; }

        public double Amount { get; set; }

        public Address? DeliveryAddress { get; set; }

        public bool IsDelivered { get; set; } = false;

        public DateTime OrderDate { get; set; }
    }
}
