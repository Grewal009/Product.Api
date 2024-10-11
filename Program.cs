using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Product.Api;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("ProductContext");

builder.Services.AddSqlServer<MyDbContext>(connString);

// Add services to the container, including the database context
/* builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); */

builder.Services.AddControllers().AddJsonOptions(x =>
x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

// GET endpoint to retrieve all items with their related categories
app.MapGet("/items", async (MyDbContext dbContext) =>
{
    var items = await dbContext.Items
        .Include(i => i.Categories)  // Include related categories
        .ToListAsync();
    return Results.Ok(items);
});

// GET endpoint to retrieve all categories with their related items
app.MapGet("/categories", async (MyDbContext dbContext) =>
{
    var categories = await dbContext.Categories
        .Include(c => c.Item)  // Include the related item
        .ToListAsync();
    return Results.Ok(categories);
});

// POST endpoint to create a new Item
app.MapPost("/items", async (MyDbContext dbContext, Item item) =>
{
    // Add the item to the database context
    dbContext.Items.Add(item);

    // Save changes to the database
    await dbContext.SaveChangesAsync();

    // Return the created item with a 201 Created response
    return Results.Created($"/items/{item.Id}", item);
});


app.Run();