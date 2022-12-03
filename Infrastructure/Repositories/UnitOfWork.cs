
using Application.Interfaces.IRepositories;
using Core.Interfaces.IRepositories;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }

        public IRefreshTokensRepository RefreshTokensRepository => new RefreshTokensRepository(_applicationDbContext);

        public async Task<bool> Complete()
        {
            return (await _applicationDbContext.SaveChangesAsync() > 0);
        }
        public bool HasChanges()
        {
            _applicationDbContext.ChangeTracker.DetectChanges();
            var changes = _applicationDbContext.ChangeTracker.HasChanges();

            return changes;
        }
        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }

    }
}
