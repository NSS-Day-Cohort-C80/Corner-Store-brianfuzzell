using Microsoft.EntityFrameworkCore;
using CornerStore.Models;
public class CornerStoreDbContext : DbContext
{
    public DbSet<Cashier> Cashiers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<Product> Products { get; set; }

    public CornerStoreDbContext(DbContextOptions<CornerStoreDbContext> context) : base(context)
    {

    }

    //allows us to configure the schema when migrating as well as seed data
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Since OrderProduct doesn't have an Id property, we add a composite key here to combine ProductId and OrderId together
        modelBuilder.Entity<OrderProduct>().HasKey(op => new { op.OrderId, op.ProductId });
        
        modelBuilder.Entity<Cashier>().HasData(new Cashier[]
        {
            new Cashier { Id = 1, FirstName = "Alice",   LastName = "Johnson" },
            new Cashier { Id = 2, FirstName = "Brian",   LastName = "Martinez" },
            new Cashier { Id = 3, FirstName = "Carol",   LastName = "Thompson" },
            new Cashier { Id = 4, FirstName = "David",   LastName = "Lee" },
            new Cashier { Id = 5, FirstName = "Maria",   LastName = "Garcia" },
        });

        modelBuilder.Entity<Category>().HasData(new Category[]
        {
            new Category { Id = 1, CategoryName = "Snacks" },
            new Category { Id = 2, CategoryName = "Beverages" },
            new Category { Id = 3, CategoryName = "Magazines" },
            new Category { Id = 4, CategoryName = "Frozen Foods" },
            new Category { Id = 5, CategoryName = "Candy" },
        });

        modelBuilder.Entity<Product>().HasData(new Product[]
        {
            new Product { Id = 1,  ProductName = "Potato Chips",        Price = 1.99m,  Brand = "Lays",          CategoryId = 1 },
            new Product { Id = 2,  ProductName = "Corn Chips",          Price = 2.49m,  Brand = "Fritos",        CategoryId = 1 },
            new Product { Id = 3,  ProductName = "Cola",                Price = 1.79m,  Brand = "Coca-Cola",     CategoryId = 2 },
            new Product { Id = 4,  ProductName = "Orange Juice",        Price = 2.99m,  Brand = "Tropicana",     CategoryId = 2 },
            new Product { Id = 5,  ProductName = "Sports Illustrated",  Price = 5.99m,  Brand = "SI",            CategoryId = 3 },
            new Product { Id = 6,  ProductName = "Frozen Burrito",      Price = 3.49m,  Brand = "El Monterey",   CategoryId = 4 },
            new Product { Id = 7,  ProductName = "Gummy Bears",         Price = 1.49m,  Brand = "Haribo",        CategoryId = 5 },
            new Product { Id = 8,  ProductName = "Chocolate Bar",       Price = 1.29m,  Brand = "Hershey's",     CategoryId = 5 },
            new Product { Id = 9,  ProductName = "Energy Drink",        Price = 3.29m,  Brand = "Red Bull",      CategoryId = 2 },
            new Product { Id = 10, ProductName = "Frozen Pizza",        Price = 5.49m,  Brand = "DiGiorno",      CategoryId = 4 },
        });

        modelBuilder.Entity<Order>().HasData(new Order[]
        {
            new Order { Id = 1, CashierId = 1, PaidOnDate = new DateTime(2024, 1, 15) },
            new Order { Id = 2, CashierId = 1, PaidOnDate = new DateTime(2024, 2,  3) },
            new Order { Id = 3, CashierId = 2, PaidOnDate = new DateTime(2024, 3, 22) },
            new Order { Id = 4, CashierId = 3, PaidOnDate = new DateTime(2024, 4, 10) },
            new Order { Id = 5, CashierId = 4, PaidOnDate = new DateTime(2024, 5,  7) },
            new Order { Id = 6, CashierId = 5, PaidOnDate = new DateTime(2024, 6, 19) },
        });

        modelBuilder.Entity<OrderProduct>().HasData(new OrderProduct[]
        {
            new OrderProduct { OrderId = 1, ProductId = 1,  Quantity = 2 },
            new OrderProduct { OrderId = 1, ProductId = 3,  Quantity = 1 },
            new OrderProduct { OrderId = 2, ProductId = 9,  Quantity = 2 },
            new OrderProduct { OrderId = 2, ProductId = 7,  Quantity = 3 },
            new OrderProduct { OrderId = 3, ProductId = 5,  Quantity = 1 },
            new OrderProduct { OrderId = 3, ProductId = 4,  Quantity = 2 },
            new OrderProduct { OrderId = 4, ProductId = 6,  Quantity = 1 },
            new OrderProduct { OrderId = 4, ProductId = 8,  Quantity = 2 },
            new OrderProduct { OrderId = 5, ProductId = 10, Quantity = 1 },
            new OrderProduct { OrderId = 6, ProductId = 2,  Quantity = 1 },
            new OrderProduct { OrderId = 6, ProductId = 3,  Quantity = 3 },
        });
    }
}
