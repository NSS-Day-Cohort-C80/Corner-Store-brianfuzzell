using CornerStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CornerStore.Models.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core and provides dummy value for testing
builder.Services.AddNpgsql<CornerStoreDbContext>(builder.Configuration["CornerStoreDbConnectionString"] ?? "testing");

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/cashiers", (CornerStoreDbContext db, Cashier cashier) =>
{
    db.Cashiers.Add(cashier);
    db.SaveChanges();
    return Results.Created($"/api/cashiers/{cashier.Id}", cashier);
});

app.MapGet("/api/cashiers/{id}", (IMapper mapper, CornerStoreDbContext db, int id) =>
{
    var cashier = db.Cashiers
    .Where(c => c.Id == id)
    .Include(c => c.Orders)
        .ThenInclude(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
    .ProjectTo<CashierDTO>(mapper.ConfigurationProvider)
    .SingleOrDefault();

    return cashier != null ? Results.Ok(cashier) : Results.NotFound();
});

app.MapGet("/api/products", (IMapper mapper, CornerStoreDbContext db, string? search) =>
{
    var query = db.Products
    .Where(p => search == null || p.ProductName.ToLower().Contains(search.ToLower()) || p.Category.CategoryName.ToLower().Contains(search.ToLower()))
    .Include(p => p.Category);

    return query.ProjectTo<ProductDTO>(mapper.ConfigurationProvider).ToList();
});

app.MapPost("/api/products", (CornerStoreDbContext db, Product product) =>
{
    db.Products.Add(product);
    db.SaveChanges();
    return Results.Created($"/api/products/{product.Id}", product);
});

app.MapPut("/api/products/{id}", (CornerStoreDbContext db, Product product, int id) =>
{
    Product productToUpdate = db.Products.SingleOrDefault(product => product.Id == id);
    if (productToUpdate == null)
    {
        return Results.NotFound();
    }
    productToUpdate.ProductName = product.ProductName;
    productToUpdate.Price = product.Price;
    productToUpdate.Brand = product.Brand;
    productToUpdate.CategoryId = product.CategoryId;

    db.SaveChanges();

    return Results.NoContent();
});

app.MapGet("/api/orders", (CornerStoreDbContext db, IMapper mapper, DateTime? orderDate) =>
{
    var query = db.Orders
    .Where(o => orderDate == null || (o.PaidOnDate.HasValue && o.PaidOnDate.Value.Date == orderDate.Value.Date))
    .Include(o => o.OrderProducts)
        .ThenInclude(op => op.Product);

    return query.ProjectTo<OrderDTO>(mapper.ConfigurationProvider).ToList();
});

app.MapGet("/api/orders/{id}", (IMapper mapper, CornerStoreDbContext db, int id) =>
{
    var order = db.Orders
    .Where(o => o.Id == id)
    .Include(o => o.Cashier)
    .Include(o => o.OrderProducts)
        .ThenInclude(op => op.Product)
            .ThenInclude(p => p.Category)
    .ProjectTo<OrderDTO>(mapper.ConfigurationProvider)
    .SingleOrDefault();

    return order != null ? Results.Ok(order) : Results.NotFound();
});

app.MapPost("/api/orders", (CornerStoreDbContext db, Order order) =>
{
    db.Orders.Add(order);
    db.SaveChanges();

    return Results.Created($"/api/orders/{order.Id}", order);
});

app.MapDelete("/api/orders/{id}", (CornerStoreDbContext db, int id) =>
{
    Order order = db.Orders.SingleOrDefault(o => o.Id == id);
    if (order == null)
    {
        return Results.NotFound();
    }
    db.Orders.Remove(order);
    db.SaveChanges();
    return Results.NoContent();
});

app.Run();

//don't move or change this!
public partial class Program { }