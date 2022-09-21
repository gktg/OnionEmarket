using NLayerEmarket.Persistence;
using Microsoft.EntityFrameworkCore;
using NLayerEmarket.Persistence.Contexts;
using static System.Net.Mime.MediaTypeNames;
using NLayerEmarket.Domain.Entities;
using Bogus.DataSets;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddMvc();
builder.Services.AddPersistenceServices();



var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        NLayerEmarketDbContext dbContext = scope.ServiceProvider.GetRequiredService<NLayerEmarketDbContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
        dbContext.Database.Migrate();
        Bogus(dbContext);
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Shared/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}");

app.Run();



void Bogus(NLayerEmarketDbContext context)
{
    List<Product> productList = new List<Product>();

    for (int j = 1; j < 20; j++)
    {
        string price = new Commerce().Price(1, 5000, 0, "TL");
        int b = Convert.ToInt32(price.Substring(2, price.Length - 2));
        Product product = new Product();
        product.Name = new Commerce().ProductName();
        product.Stock = j * j;
        product.Price = b;
        product.Media = new Images().PicsumUrl();
        product.CreatedDate = DateTime.Now;
        productList.Add(product);
    }
    context.Products.AddRange(productList);
    context.SaveChanges();
}