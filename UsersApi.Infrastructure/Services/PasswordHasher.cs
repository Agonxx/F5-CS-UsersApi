using UsersApi.Domain.Interfaces.Utils;

namespace UsersApi.Infrastructure.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string senha) => BCrypt.Net.BCrypt.HashPassword(senha);

        public bool VerifyPassword(string senha, string hash) => BCrypt.Net.BCrypt.Verify(senha, hash);
    }
}
