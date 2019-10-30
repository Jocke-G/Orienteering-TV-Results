using LayoutRestService.EntityFramework.Repositories;
using LayoutRestService.Models.Configuration;
using LayoutRestService.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LayoutRestService.EntityFramework
{
    public class Initializer
    {
        public static void Initialize(IServiceCollection services, DatabaseConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(c => c.UseMySql(configuration.GetConnectionString()));

            services.AddScoped<ILayoutRepository, LayoutRepository>();
        }
    }
}
