using NLayerEmarket.Persistence;
using Microsoft.EntityFrameworkCore;
using NLayerEmarket.Persistence.Contexts;
using static System.Net.Mime.MediaTypeNames;
using NLayerEmarket.Domain.Entities;
using Bogus.DataSets;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using NLayerEmarket.Domain.Enums;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddMvc();
builder.Services.AddPersistenceServices();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddSession(opt =>
{
    opt.IdleTimeout = TimeSpan.FromMinutes(30);
    opt.Cookie.Path = "/";
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.SlidingExpiration = true;
    options.Cookie.Name = "MyAppCookie";
    options.AccessDeniedPath = "/";
});


WebApplication app = builder.Build();



if (app.Environment.IsDevelopment())
{
    using (IServiceScope scope = app.Services.CreateScope())
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

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();



void Bogus(NLayerEmarketDbContext context)
{
    string[] categoryArray = new Commerce("tr").Categories(10);
    for (int i = 5; i < 10; i++)
    {
        List<Product> productList = new List<Product>();

        Category category = new Category();
        category.Name = categoryArray[i];
        category.Description = new Lorem("tr").Sentence(10);
        category.Status = DataStatus.Inserted;
        category.CreatedDate = DateTime.Now;


        for (int j = 5; j < 20; j++)
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
        category.Products = productList;
        context.Categorys.Add(category);
        context.SaveChanges();
    }


    var user = new User();
    user.Name = "Servet G�ktu�";
    user.Surname = "T�rkan";
    user.Mail = "g@mail.com";
    user.Password = "+hBCBANDrxIZaywz3zTgEzSrJWuH0QEXdL6Kvku6Wic=";
    user.Status = DataStatus.Inserted;
    user.Role = Role.Admin;
    user.CreatedDate = DateTime.Now;
    context.Users.Add(user);
    context.SaveChanges();

}