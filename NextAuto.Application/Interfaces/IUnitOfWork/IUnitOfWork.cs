

using NextAuto.Domain.Entities;
using NextAuto.Domain.IRepositories;

namespace NextAuto.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<Car> Cars { get; }

    IRepository<Client> Clients { get; }

    Task<int> SaveChangesAsync();

    Task BeginTransactionAsync();

    Task CommitTransactionAsync();

    Task RollbackTransactionAsync();
}
