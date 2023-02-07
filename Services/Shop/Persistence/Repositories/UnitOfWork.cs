using Shop.Core.Entities;

using Shop.Core.Interfaces;
using System.Collections;

namespace Shop.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly StoreContext _context;
    private readonly Hashtable _repositories;

    public UnitOfWork(StoreContext context)
    {
        _context = context;
        _repositories ??= new Hashtable();
    }

    public async Task<int> Complete() { return await _context.SaveChangesAsync(); }

    public void Dispose() { _context.Dispose(); }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        var type = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

            _repositories.Add(type, repositoryInstance);
        }

        return (IGenericRepository<TEntity>)_repositories[type]!;
    }

    public bool HasChanges() { return _context.ChangeTracker.HasChanges(); }
}