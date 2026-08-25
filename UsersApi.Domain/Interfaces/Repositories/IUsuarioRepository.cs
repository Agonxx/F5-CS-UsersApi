using UsersApi.Domain.Entities;

namespace UsersApi.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetByEmail(string email);
        Task<Usuario> GetMe();
        Task<bool> Create(Usuario usuarioObj);
        Task<bool> EmailExists(string email);
        Task<bool> CpfExists(string cpf);
    }
}
