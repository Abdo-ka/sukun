namespace Sukun.Application.Interfaces
{
    public interface IDataVersionService
    {
        Task<Dictionary<string, long>> GetAllVersionsAsync();
    }
}