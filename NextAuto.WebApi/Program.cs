using Microsoft.EntityFrameworkCore;
using NextAuto.Application;
using NextAuto.Infrastructure;
using NextAuto.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    
    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        // Указываем сборку с миграциями (Infrastructure)
        npgsqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
        
        // Опционально: настройки для PostgreSQL
        npgsqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorCodesToAdd: null);
    });
    
    // Опционально: логирование SQL запросов (только для разработки)
    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    }
});

var app = builder.Build();

app.MapControllers();

app.Run();