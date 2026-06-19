using Microsoft.EntityFrameworkCore.Storage;
using NextAuto.Application.Interfaces;
using NextAuto.Domain.Entities;
using NextAuto.Domain.IRepositories;
using NextAuto.Infrastructure.Data.Repositories;

namespace NextAuto.Infrastructure.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _transaction;

        

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Cars = new Repository<Car>(_context);
        Clients = new Repository<Client>(_context);
    }

    public IRepository<Car> Cars { get; }

    public IRepository<Client> Clients { get; }


    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
            await _transaction.CommitAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
            await _transaction.RollbackAsync();
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}