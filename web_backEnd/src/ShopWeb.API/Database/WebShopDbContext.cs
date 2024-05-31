
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ShopWeb.API.Helper;
using ShopWeb.Core.src.Entity;

namespace ShopWeb.API.Database
{
    public class WebShopDbContext : DbContext
    {
        public WebShopDbContext(DbContextOptions<WebShopDbContext> options)
            : base(options) { }

        public DbSet<Category> Categories { get; set; }

        public DbSet<User> User { get; set; }
        public DbSet<Address> Addresses { get; set; } = null!;

        public DbSet<Product> Products { get; set; }

        public DbSet<ImageUrl> ImageUrls { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderedLineItem> OrderedLineItems { get; set; }

        public DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // db connection string: host; server name; username; pw; db name
            optionsBuilder.UseNpgsql("Host=ep-shrill-star-a29vaqnh.eu-central-1.aws.neon.tech;Username=Shopweb_owner;Database=Shopweb;Password=CWr9AKzYEOJ3;SslMode=Require"); // PostgreSQL
            optionsBuilder.UseSnakeCaseNamingConvention(); // convert C# class names to snake_case table names in database
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Description).IsRequired();
                entity.Property(e => e.Image).IsRequired();
                entity.HasIndex(e => e.Name).IsUnique();
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName); // .IsRequired();
                entity.Property(e => e.LastName); // .IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.Phone); // .IsRequired();
                entity.Property(e => e.Role).IsRequired();
                entity.HasIndex(e => e.Email).IsUnique();
            });
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.StreetName).IsRequired();
                entity.Property(e => e.StreetNumber);
                entity.Property(e => e.UnitNumber);
                entity.Property(e => e.PostalCode).IsRequired();
                entity.Property(e => e.City).IsRequired();
                entity.Property(e => e.UserId).IsRequired();
            });
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.CategoryId).IsRequired();
                entity.Property(e => e.Price).IsRequired();
                entity.Property(e => e.Description).IsRequired();
            });
            modelBuilder.Entity<ImageUrl>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ProductId).IsRequired();
                entity.Property(e => e.Url).IsRequired();
            });
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.AddressId).IsRequired();
                entity.Property(e => e.OrderDate).IsRequired();
                entity.HasMany(e => e.OrderedLineItems).WithOne(e => e.Order).HasForeignKey(e => e.OrderId);
            });
            modelBuilder.Entity<OrderedLineItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.OrderId).IsRequired();
                entity.Property(e => e.ProductId).IsRequired();
                entity.Property(e => e.Quantity).IsRequired();
                entity.HasOne(oli => oli.Order)
                        .WithMany(o => o.OrderedLineItems)
                        .HasForeignKey(oli => oli.OrderId);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.ProductId).IsRequired();
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.Rating).IsRequired();
                entity.Property(e => e.Comment).IsRequired();
            });

            // SeedAddressData(modelBuilder);
            // SeedCategoryData(modelBuilder);
            // SeedUserData(modelBuilder);
            // SeedProductData(modelBuilder);
            // SeedOrderData(modelBuilder);
            // SeedProductImageData(modelBuilder);
            // SeedOrderLineItemData(modelBuilder);
            // SeedReviewData(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        private void SeedAddressData(ModelBuilder modelBuilder)
        {
            var addresses = CsvReaderHelper.ReadAddressesFromCsv("./Database/data/addresses.csv");

            modelBuilder.Entity<Address>().HasData(addresses);
        }
        private void SeedCategoryData(ModelBuilder modelBuilder)
        {
            var categories = CsvReaderHelper.ReadCategoriesFromCsv("./Database/data/categories.csv");
            modelBuilder.Entity<Category>().HasData(categories);
        }
        private void SeedUserData(ModelBuilder modelBuilder)
        {
            var users = CsvReaderHelper.ReadUsersFromCsv("./Database/data/userHash.csv");
            modelBuilder.Entity<User>().HasData(users);
        }
        private void SeedProductData(ModelBuilder modelBuilder)
        {
            var products = CsvReaderHelper.ReadProductsFromCsv("./Database/data/products.csv");
            modelBuilder.Entity<Product>().HasData(products);
        }
        private void SeedProductImageData(ModelBuilder modelBuilder)
        {
            var images = CsvReaderHelper.ReadProductImagesFromCsv("./Database/data/productImages.csv");
            modelBuilder.Entity<ImageUrl>().HasData(images);
        }
        private void SeedOrderData(ModelBuilder modelBuilder)
        {
            var orders = CsvReaderHelper.ReadOrdersFromCsv("./Database/data/order.csv");
            modelBuilder.Entity<Order>().HasData(orders);
        }
        private void SeedOrderLineItemData(ModelBuilder modelBuilder)
        {
            var orderLineItems = CsvReaderHelper.ReadOrderLineItemsFromCsv("./Database/data/orderItem.csv");
            modelBuilder.Entity<OrderedLineItem>().HasData(orderLineItems);
        }
    }

}