using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NextAuto.Application.Interfaces;
using NextAuto.Domain.IRepositories;
using NextAuto.Infrastructure.Data;
using NextAuto.Infrastructure.Data.Repositories;
using NextAuto.Infrastructure.Data.UnitOfWork;

namespace NextAuto.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        Action<DbContextOptionsBuilder> dbContextOptions)
    {
        // Регистрируем DbContext
        services.AddDbContext<ApplicationDbContext>(dbContextOptions);
        
        // Регистрируем интерфейс
        services.AddScoped<ApplicationDbContext>(provider => 
            provider.GetRequiredService<ApplicationDbContext>());
        
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}