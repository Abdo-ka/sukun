using Sukun.Application.Interfaces;

namespace Sukun.Application.Implemantation
{
    public class BCryptPasswordHasher : IPasswordHasher
    {
        //TODO 
        public string HashPassword(string password)
        {
            return null; // BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return true; // BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}