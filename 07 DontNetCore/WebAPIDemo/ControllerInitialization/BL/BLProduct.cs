using ControllerInitialization.Models;

namespace ControllerInitialization.BL
{
    /// <summary>
    /// BL class for Product
    /// </summary>
    public class BLProduct
    {
        /// <summary>
        /// List of Product
        /// </summary>
        public static List<PRD01>? lstProduct = null;

        /// <summary>
        /// Next Product Id
        /// </summary>
        public static int NextId = 0;

        /// <summary>
        /// BLProduct static constructor
        /// </summary>
        static BLProduct()
        {
            lstProduct = new List<PRD01>()
            {
                new PRD01 { d01f01 = 1, d01f02 = "MRF Bat", d01f03 = 899, d01f04 = 160},
                new PRD01 { d01f01 = 2, d01f02 = "Tennis Ball", d01f03 = 249, d01f04 = 1000},
                new PRD01 { d01f01 = 3, d01f02 = "Black T-Shirt", d01f03 = 1299, d01f04 = 50},
                new PRD01 { d01f01 = 4, d01f02 = "Adapter", d01f03 = 349, d01f04 = 50}
            };
            NextId = 5;
        }

        /// <summary>
        /// Method to fetch list of product
        /// </summary>
        /// <returns></returns>
        public List<PRD01>? FetchProductList()
        {
            return lstProduct;
        }

        /// <summary>
        /// Method to check if the new product item is correctly configured
        /// </summary>
        /// <param name="product">PRD01 object</param>
        /// <returns>True if new product item is correctly configure else false</returns>
        private bool Validate(PRD01 product)
        {
            if (string.IsNullOrEmpty(product.d01f02) || product.d01f03 < 0 || product.d01f04 < 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Method to perform operation(s) on new product item before save
        /// </summary>
        /// <param name="product">PRD01 object</param>
        private void PreSave(PRD01 product)
        {
            product.d01f01 = NextId;
        }

        /// <summary>
        /// Method to save the new product item in product list
        /// </summary>
        /// <param name="product">PRD01 object</param>
        private void Save(PRD01 product)
        {
            lstProduct?.Add(product);
            NextId++;
        }

        /// <summary>
        /// Method to add new product item to product list
        /// </summary>
        /// <param name="product">PRD01 object</param>
        public void AddProduct(PRD01 product)
        {
            if (Validate(product))
            {
                PreSave(product);
                Save(product);
            }
        }
    }
}
