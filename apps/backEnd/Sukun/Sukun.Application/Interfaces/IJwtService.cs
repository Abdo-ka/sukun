namespace Sukun.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Guid adminId, string email, string role);
    }
}