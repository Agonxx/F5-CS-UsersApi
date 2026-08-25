namespace UsersApi.Domain.Interfaces.Utils
{
    public interface IPasswordHasher
    {
        string HashPassword(string senha);
        bool VerifyPassword(string senha, string hash);
    }
}
