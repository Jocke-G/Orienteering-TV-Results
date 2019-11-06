using LayoutService.DataAccess.EntityFramework;
using LayoutService.DataAccess.Repositories;
using LayoutService.DataAccess.RepositoryInterfaces;
using LayoutService.ServiceBooter.Model.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LayoutService.ServiceBooter
{
    public class DatabaseInitializer
    {
        public static void Initialize(IServiceCollection services, DatabaseConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(c => c.UseMySql(configuration.GetConnectionString()));

            services.AddScoped<ILayoutRepository, LayoutRepository>();
        }
    }
}
