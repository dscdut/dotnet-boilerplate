namespace DotnetBoilerplate.Application.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        Task<int> SaveChangesAsync();
        Task CommitAsync();
        void BeginTransaction();
        void Rollback();
    }
}
