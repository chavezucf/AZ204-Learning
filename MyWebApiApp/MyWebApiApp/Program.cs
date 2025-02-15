using System.Text;
using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyWebApiApp.Data;  // Ensure this matches your project

var builder = WebApplication.CreateBuilder(args);
// Load Azure Key Vault secrets
// var keyVaultName = Environment.GetEnvironmentVariable("AZURE_KEYVAULT_NAME");
// if (!string.IsNullOrEmpty(keyVaultName))
// {
//     var keyVaultUri = new Uri($"https://{keyVaultName}.vault.azure.net");
//
//     builder.Configuration.AddAzureKeyVault(
//         keyVaultUri,
//         new DefaultAzureCredential()
//     );
// }


var isDevelopment = builder.Environment.IsDevelopment();
var connectionString = isDevelopment 
    ? builder.Configuration.GetConnectionString("LocalDb") 
    : builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// Enable serving static files from wwwroot
app.UseStaticFiles();

// Redirect root to index.html
app.UseDefaultFiles();
app.UseStaticFiles();

// ✅ Inject User Data into the HTML File
app.MapGet("/", async ([FromServices] AppDbContext db) =>
{
    var firstUser = await db.Users.FirstOrDefaultAsync();
    var userName = firstUser?.Name ?? "No users found";

    // Read the HTML file
    var htmlTemplate = await File.ReadAllTextAsync("wwwroot/index.html");

    // Replace {{user}} placeholder
    var finalHtml = htmlTemplate.Replace("{{user}}", userName);

    return Results.Text(finalHtml, "text/html", Encoding.UTF8);
});

// ✅ API Endpoint: Get All Users
app.MapGet("/api/users", async ([FromServices] AppDbContext db) =>
{
    var users = await db.Users.ToListAsync();
    return Results.Ok(users);
});

// ✅ API Endpoint: Get All Products
app.MapGet("/api/products", async ([FromServices] AppDbContext db) =>
{
    var products = await db.Products.ToListAsync();
    return Results.Ok(products);
});


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!db.Users.Any())
    {
        db.Users.Add(new User { Name = "John Doe" });
        db.SaveChanges();
    }
    
    if (!db.Products.Any())
    {
        db.Products.Add(new Product() { Name = "The 100", Price = 100 });
        db.SaveChanges();
    }
}

app.Run();
