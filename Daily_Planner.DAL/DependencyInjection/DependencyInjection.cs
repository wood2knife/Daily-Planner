using Daily_Planner.DAL.Interceptors;
using Daily_Planner.DAL.Repositories;
using Daily_Planner.Domain.Entity;
using Daily_Planner.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Daily_Planner.DAL.DependencyInjection;

public static class DependencyInjection
{
    public static void AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MSSQL");
        services.AddSingleton<DateInterceptor>();
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        services.InitRepositories();
    }

    private static void InitRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
        services.AddScoped<IBaseRepository<Report>, BaseRepository<Report>>();
    }
}