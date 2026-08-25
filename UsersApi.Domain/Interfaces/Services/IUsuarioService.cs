using UsersApi.Domain.DTOs;

namespace UsersApi.Domain.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<string> AuthAsync(string email, string senha);
        Task<UsuarioResponse> GetMeAsync();
        Task<bool> CadastrarDoadorAsync(CadastroDoadorRequest request);
    }
}
