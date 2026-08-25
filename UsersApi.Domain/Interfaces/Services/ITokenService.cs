namespace UsersApi.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        string GerarToken(int id, string nome, string email, ERole role, DateTime cadastradoEm);
    }
}
