using DgServer.Models.Entity;

namespace DgServer.Models.Domain
{
    public class ProductQuantity : Product
    {
        public int Quantity { get; set; }

        public ProductQuantity(Product product, int quantity)
        {
            if (product != null)
            {
                this.Id = product.Id;
                this.Name = product.Name;
                this.Price = product.Price;
                this.Category = product.Category;
                this.Quantity = quantity;
            }
        }
    }
}
