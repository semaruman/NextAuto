

using NextAuto.Domain.Entities;

namespace NextAuto.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<Car> Cars { get; }

    Task<int> SaveChangesAsync();

    Task BeginTransactionAsync();

    Task CommitTransactionAsync();

    Task RollbackTransactionAsync();
}
