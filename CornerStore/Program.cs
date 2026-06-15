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

app.Run();

//don't move or change this!
public partial class Program { }