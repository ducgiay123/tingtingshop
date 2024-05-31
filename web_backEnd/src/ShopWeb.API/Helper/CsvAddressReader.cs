using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopWeb.Core.src.Entity;
using System.IO;
using CsvHelper;
using ShopWeb.Core.src.Role;
using System.Globalization;
namespace ShopWeb.API.Helper
{
    public class CsvReaderHelper
    {
        public static List<Address> ReadAddressesFromCsv(string filePath)
        {
            List<Address> addresses = new List<Address>();

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var address = new Address
                    {
                        Id = Guid.Parse(csv.GetField("id")),
                        StreetName = csv.GetField("street_name"),
                        StreetNumber = csv.GetField("street_number"),
                        UnitNumber = csv.GetField("unit_number"),
                        PostalCode = csv.GetField("postal_code"),
                        City = csv.GetField("city"),
                        UserId = Guid.Parse(csv.GetField("customer_id"))
                    };
                    addresses.Add(address);
                }
            }

            return addresses;
        }
        public static List<Category> ReadCategoriesFromCsv(string filePath)
        {
            List<Category> categories = new List<Category>();

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var category = new Category
                    {
                        Id = int.Parse(csv.GetField("id")),
                        Name = csv.GetField("name"),
                        Description = csv.GetField("description"),
                        Image = csv.GetField("image")
                    };
                    categories.Add(category);
                }
            }

            return categories;
        }
        public static List<User> ReadUsersFromCsv(string filePath)
        {
            List<User> users = new List<User>();

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var user = new User
                    {
                        Id = Guid.Parse(csv.GetField("id")),
                        FirstName = csv.GetField("first_name"),
                        LastName = csv.GetField("last_name"),
                        Email = csv.GetField("email"),
                        PasswordHash = csv.GetField("password_hash"),
                        Phone = csv.GetField("phone"),
                        Role = (UserRole)int.Parse(csv.GetField("role")),
                        AvatarLink = csv.GetField("avatar_link")
                    };
                    users.Add(user);
                }
            }

            return users;
        }
        public static List<Product> ReadProductsFromCsv(string filePath)
        {
            List<Product> products = new List<Product>();

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var product = new Product
                    {
                        Id = csv.GetField<Guid>("id"),
                        Name = csv.GetField<string>("name"),
                        CategoryId = csv.GetField<int>("category_id"),
                        Price = csv.GetField<decimal>("price"),
                        Description = csv.GetField<string>("description")
                    };
                    products.Add(product);
                }
            }
            return products;
        }
        public static List<Order> ReadOrdersFromCsv(string filePath)
        {
            List<Order> orders = new List<Order>();

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var order = new Order
                    {
                        Id = csv.GetField<Guid>("id"),
                        UserId = csv.GetField<Guid>("user_id"),
                        OrderDate = DateTime.SpecifyKind(csv.GetField<DateTime>("order_date"), DateTimeKind.Utc),
                        AddressId = csv.GetField<Guid>("address_id")
                    };
                    orders.Add(order);
                }
            }
            return orders;
        }
        public static List<OrderedLineItem> ReadOrderLineItemsFromCsv(string filePath)
        {
            List<OrderedLineItem> orderLineItems = new List<OrderedLineItem>();

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var orderLineItem = new OrderedLineItem
                    {
                        Id = csv.GetField<Guid>("id"),
                        OrderId = csv.GetField<Guid>("order_id"),
                        ProductId = csv.GetField<Guid>("product_id"),
                        Price = csv.GetField<decimal>("price"),
                        Quantity = csv.GetField<int>("quantity")
                    };
                    orderLineItems.Add(orderLineItem);
                }
            }

            return orderLineItems;
        }
        public static List<ImageUrl> ReadProductImagesFromCsv(string filePath)
        {
            List<ImageUrl> images = new List<ImageUrl>();

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var image = new ImageUrl
                    {
                        Id = csv.GetField<Guid>("id"),
                        ProductId = csv.GetField<Guid>("product_id"),
                        Url = csv.GetField<string>("url")
                    };
                    images.Add(image);
                }
            }

            return images;
        }
    }
}