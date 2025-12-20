namespace Sukun.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendWelcomeEmailAsync(string email, string name);
        Task SendPasswordResetEmailAsync(string email, string resetToken);
    }
}