using NLayerEmarket.Persistence;
using Microsoft.EntityFrameworkCore;
using NLayerEmarket.Persistence.Contexts;
using static System.Net.Mime.MediaTypeNames;
using NLayerEmarket.Domain.Entities;
using Bogus.DataSets;
using Microsoft.AspNetCore.Http;
using JavaScriptEngineSwitcher.V8;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using React.AspNet;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddReact();

// Make sure a JS engine is registered, or you will get an error!
builder.Services.AddJsEngineSwitcher(options => options.DefaultEngineName = V8JsEngine.EngineName)
  .AddV8();
builder.Services.AddControllersWithViews();

builder.Services.AddMvc();
builder.Services.AddPersistenceServices();



var app = builder.Build();



//if (app.Environment.IsDevelopment())
//{
//    using (var scope = app.Services.CreateScope())
//    {
//        NLayerEmarketDbContext dbContext = scope.ServiceProvider.GetRequiredService<NLayerEmarketDbContext>();
//        dbContext.Database.EnsureDeleted();
//        dbContext.Database.EnsureCreated();
//        dbContext.Database.Migrate();
//        Bogus(dbContext);
//    }
//}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Shared/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();

// Initialise ReactJS.NET. Must be before static files.
app.UseReact(config =>
{
    // If you want to use server-side rendering of React components,
    // add all the necessary JavaScript files here. This includes
    // your components as well as all of their dependencies.
    // See http://reactjs.net/ for more information. Example:
    //config
    //  .AddScript("~/js/First.jsx")
    //  .AddScript("~/js/Second.jsx");

    // If you use an external build too (for example, Babel, Webpack,
    // Browserify or Gulp), you can improve performance by disabling
    // ReactJS.NET's version of Babel and loading the pre-transpiled
    // scripts. Example:
    //config
    //  .SetLoadBabel(false)
    //  .AddScriptWithoutTransform("~/js/bundle.server.js");
});
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