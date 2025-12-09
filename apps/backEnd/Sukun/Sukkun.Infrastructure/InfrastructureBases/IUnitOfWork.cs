namespace Sukun.Infrastructure.InfrastructureBases
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CompleteAsync();
    }
}
