using Application.Interfaces.IRepositories;

namespace Core.Interfaces.IRepositories
{
    public interface IUnitOfWork:IDisposable
    {
        IRefreshTokensRepository RefreshTokensRepository { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}
