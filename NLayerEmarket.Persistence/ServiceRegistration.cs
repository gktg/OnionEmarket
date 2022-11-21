using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using NLayerEmarket.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NLayerEmarket.Application.Repositories;
using NLayerEmarket.Persistence.Repositories;
using NLayerEmarket.Application.Collections.Basket;
using NLayerEmarket.Persistence.Collections.Basket;
using MongoDB.Driver;

namespace NLayerEmarket.Persistence
{
    public static class ServiceRegistration
    {

        public static void AddPersistenceServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<NLayerEmarketDbContext>(options => options.UseSqlServer("Data Source=GKTGPC;Initial Catalog=NLayerEmarket;Integrated Security=True;"), ServiceLifetime.Singleton);    


            services.AddSingleton<IUserReadRepository,UserReadRepository>();
            services.AddSingleton<IUserWriteRepository,UserWriteRepository>();
                     
            services.AddSingleton<IProductReadRepository, ProductReadRepository>();
            services.AddSingleton<IProductWriteRepository, ProductWriteRepository>();

            services.AddSingleton<ICategoryReadRepository, CategoryReadRepository>();
            services.AddSingleton<ICategoryWriteRepository, CategoryWriteRepository>();
            services.AddSingleton<IBasketCollection, BasketCollection>();

            services.AddSingleton<IMongoClient>(s => new MongoClient(configuration.GetRequiredSection("MongoDbConnectionString").Value));


        }
    }
}
