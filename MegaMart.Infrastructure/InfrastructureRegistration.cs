using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaMart.Core.Interfaces;
using MegaMart.Core.Services;
using MegaMart.Infrastructure.Data;
using MegaMart.Infrastructure.Repositories;
using MegaMart.Infrastructure.Repositories.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace MegaMart.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.AddSingleton<IImageManagementService, ImageManagementService>();

            services.AddDbContext<AppDbContext>(op =>
            {
                op.UseSqlServer(configuration.GetConnectionString("MegaMartDatabase"));
            });
            return services;
        }
    }
}
