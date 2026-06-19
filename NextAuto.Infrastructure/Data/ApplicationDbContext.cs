using Microsoft.EntityFrameworkCore;
using NextAuto.Domain.Entities;

namespace NextAuto.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Car> Cars { get; set; }
    
    public DbSet<Client> Clients { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}