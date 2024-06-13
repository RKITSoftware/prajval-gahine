using DgServer.Models.Entity;
using DgServer.Models.Domain;

namespace DgServer.Data
{
    public class DataStore
    {
        public List<User> LstUser { get; set; }
        public int preUserCount = 30;
        public int nextUserId;

        public List<Product> LstProduct { get; set; }
        public int preProductCount = 50;
        public int nextProductId;

        public List<Order> LstOrder { get; set; }
        public int preOrderCount = 0;
        public int nextOrderId;

        public DataStore()
        {
            nextUserId = preUserCount + 1;
            nextProductId = preProductCount + 1;
            nextOrderId = preOrderCount + 1;

            LstUser = new List<User>();
            Random rand = new Random();

            string[] streets = { "Govind Nagar", "MG Road", "Station Road", "Park Street" };
            string[] towns = { "Thane", "Andheri", "Bandra", "Borivali" };
            string[] cities = { "Mumbai", "Pune", "Nagpur", "Nashik" };
            string[] states = { "Maharashtra", "Gujarat", "Karnataka", "Tamil Nadu" };

            for (int i = 1; i <= preUserCount; i++)
            {
                LstUser.Add(new User()
                {
                    Id = i,
                    Email = $"u{i}@gmail.com",
                    Name = $"u{i}",
                    PermanentAddress = new Address()
                    {
                        PlotNo = rand.Next(1, 1000),
                        Street = streets[rand.Next(streets.Length)],
                        Town = towns[rand.Next(towns.Length)],
                        City = cities[rand.Next(cities.Length)],
                        State = states[rand.Next(states.Length)],
                        Pincode = $"{rand.Next(100000, 999999)}"
                    }
                });
            }

            /*
            LstUser = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    Email = "user1@gmail.com",
                    Name = "User1 DG",
                    PermanentAddress = new Address()
                    {
                        PlotNo = 102,
                        Street = "Govind Nagar",
                        Town = "Thane",
                        City = "Mumbai",
                        State = "Maharashtra",
                        Pincode = "400525"
                    }
                },
                new User()
                {
                    Id = 2,
                    Email = "user2@gmail.com",
                    Name = "User2 DG",
                    PermanentAddress = new Address()
                    {
                        PlotNo = 102,
                        Street = "Vrundavan",
                        Town = "Bhestan",
                        City = "Surat",
                        State = "Gujarat",
                        Pincode = "395023"
                    }
                },
                new User()
                {
                    Id = 3,
                    Email = "user3@gmail.com",
                    Name = "User3 DG",
                    PermanentAddress = new Address()
                    {
                        PlotNo = 103,
                        Street = "At. Post Piplod",
                        Town = "Bhaupur",
                        City = "Ujjain",
                        State = "Madhya Pradesh",
                        Pincode = "263547"
                    }
                },
                new User()
                {
                    Id = 4,
                    Email = "user4@gmail.com",
                    Name = "User4 DG",
                    PermanentAddress = new Address()
                    {
                        PlotNo = 104,
                        Street = "Ganesh Park",
                        Town = "Ghatpur",
                        City = "Nagpur",
                        State = "Maharashtra",
                        Pincode = "124525"
                    }
                },
                new User()
                {
                    Id = 5,
                    Email = "user5@gmail.com",
                    Name = "User5 DG",
                    PermanentAddress = new Address()
                    {
                        PlotNo = 105,
                        Street = "Mantod",
                        Town = "Bilas Nagar",
                        City = "Visakhapatnam",
                        State = "Andhra Pradesh",
                        Pincode = "785699"
                    }
                },
                new User()
                {
                    Id = 6,
                    Email = "user6@gmail.com",
                    Name = "User6 DG",
                    PermanentAddress = new Address()
                    {
                        PlotNo = 106,
                        Street = "Lakshdeep Street",
                        Town = "Bhatod",
                        City = "Agra",
                        State = "Uttar Pradesh",
                        Pincode = "395023"
                    }
                },
                new User()
                {
                    Id = 7,
                    Email = "user7@gmail.com",
                    Name = "User7 DG",
                    PermanentAddress = new Address()
                    {
                        PlotNo = 107,
                        Street = "Premnagar",
                        Town = "Sherpur",
                        City = "Kalyan-Dombivali",
                        State = "Maharashtra",
                        Pincode = "400005"
                    }
                },
                new User()
                {
                    Id = 8,
                    Email = "user8@gmail.com",
                    Name = "User8 DG",
                    PermanentAddress = new Address()
                    {
                        PlotNo = 108,
                        Street = "Maruti Nagar",
                        Town = "Omnagar",
                        City = "Rajkot",
                        State = "Gujarat",
                        Pincode = "450069"
                    }
                }
            };
            */

            /*
            LstProduct = new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Name = "IPhone 15",
                    Price = 79999,
                    Category = "Mobile"
                },
                new Product()
                {
                    Id = 2,
                    Name = "Hero splex 200",
                    Price = 12999,
                    Category = "Bike"
                },
                new Product()
                {
                    Id = 3,
                    Name = "Macbook 2 pro",
                    Price = 159999,
                    Category = "Laptop"
                },
                new Product()
                {
                    Id = 4,
                    Name = "Juicer",
                    Price = 859,
                    Category = "Household"
                },
                new Product()
                {
                    Id = 5,
                    Name = "MI Powerbank",
                    Price = 1999,
                    Category = "Mobile Accessory"
                },
                new Product()
                {
                    Id = 6,
                    Name = "Redmi 8i back cover",
                    Price = 239,
                    Category = "Mobile Accessory"
                },
                new Product()
                {
                    Id = 7,
                    Name = "Lenovo Ideapad Slim 3",
                    Price = 45999,
                    Category = "Laptop"
                },
                new Product()
                {
                    Id = 8,
                    Name = "Dell Monitor 1562",
                    Price = 16999,
                    Category = "Computer Accessory"
                },
                new Product()
                {
                    Id = 9,
                    Name = "Intel 9i CPU",
                    Price = 34599,
                    Category = "Computer Accessory"
                }
                ,new Product()
                {
                    Id = 10,
                    Name = "Oppo Enco Buds 2",
                    Price = 2999,
                    Category = "Mobile Accessory"
                },
                new Product()
                {
                    Id = 11,
                    Name = "Notebook",
                    Price = 150,
                    Category = "Stationary"
                },
                new Product()
                {
                    Id = 12,
                    Name = "Marker Pen",
                    Price = 25,
                    Category = "stationary"
                },
                new Product()
                {
                    Id = 13,
                    Name = "IPhone 15",
                    Price = 79999,
                    Category = "Mobile"
                },
                new Product()
                {
                    Id = 14,
                    Name = "Mar Q Air Conditioner",
                    Price = 35999,
                    Category = "Household"
                },
                new Product()
                {
                    Id = 15,
                    Name = "refrigerator",
                    Price = 19999,
                    Category = "Household"
                },
            };
            */

            LstProduct = new List<Product>();

            string[] categories = { "Household", "Electronics", "Clothing", "Toys", "Groceries" };
            string[] names = { "Refrigerator", "Washing Machine", "Smartphone", "T-shirt", "Doll", "Milk", "Laptop", "Blender", "Sofa", "Jeans" };

            for (int i = 1; i <= preProductCount; i++)
            {
                LstProduct.Add(new Product()
                {
                    Id = i,
                    Name = names[rand.Next(names.Length)],
                    Price = rand.Next(100, 100000),
                    Category = categories[rand.Next(categories.Length)]
                });
            }
            // --------------------------------------------------------------------------------------------------------
            this.LstOrder = new List<Order>();
            Random random = new Random();

            for (int i = 0; i < 500; i++)
            {
                Order order = new Order
                {
                    Id = i, // Random order ID
                    UserId = LstUser[random.Next(1, LstUser.Count)].Id, // Random user ID
                    LstProductQuantity = new List<ProductQuantity>(), // Empty list for now
                    Amount = random.NextDouble() * 1000, // Random amount between 0 and 1000
                    DeliveryAddress = new Address()
                    {
                        PlotNo = rand.Next(1, 1000),
                        Street = streets[rand.Next(streets.Length)],
                        Town = towns[rand.Next(towns.Length)],
                        City = cities[rand.Next(cities.Length)],
                        State = states[rand.Next(states.Length)],
                        Pincode = $"{rand.Next(100000, 999999)}"
                    },
                    IsDelivered = random.Next(0, 2) == 1, // Random boolean value
                    OrderDate = DateTime.Now.AddDays(-random.Next(1, 30)) // Random date in the last 30 days
                };

                // Random number of products with random quantities
                int numProducts = random.Next(1, 5); // Random number of products between 1 and 5
                for (int j = 0; j < numProducts; j++)
                {
                    order.LstProductQuantity.Add(new ProductQuantity
                    {
                        ProductId = LstProduct[random.Next(1, LstProduct.Count)].Id, // Random product ID
                        Quantity = random.Next(1, 10) // Random quantity between 1 and 10
                    });
                }

                LstOrder.Add(order);
            }
        }
    }
}
