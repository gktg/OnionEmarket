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

namespace NLayerEmarket.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<NLayerEmarketDbContext>(options => options.UseSqlServer("Data Source=GKTGPC;Initial Catalog=NLayerEmarket;Integrated Security=True;"),ServiceLifetime.Singleton);

            services.AddSingleton<ICustomerReadRepository,CustomerReadRepository>();
            services.AddSingleton<ICustomerrWriteRepository,CustomerWriteRepository>();
                     
            services.AddSingleton<IProductReadRepository, ProductReadRepository>();
            services.AddSingleton<IProductWriteRepository, ProductWriteRepository>();
                     
            services.AddSingleton<IOrderReadRepository, OrderReadRepository>();
            services.AddSingleton<IOrderWriteRepository, OrderWriteRepository>();

            
        }
    }
}
