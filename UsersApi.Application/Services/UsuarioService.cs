using UsersApi.Domain.DTOs;
using UsersApi.Domain.Entities;
using UsersApi.Domain.Extensions;
using UsersApi.Domain.Interfaces.Repositories;
using UsersApi.Domain.Interfaces.Services;
using UsersApi.Domain.Interfaces.Utils;

namespace UsersApi.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repo;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;

        public UsuarioService(IUsuarioRepository repo, IPasswordHasher passwordHasher, ITokenService tokenService)
        {
            _repo = repo;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<string> AuthAsync(string email, string senha)
        {
            var usuario = await _repo.GetByEmail(email);

            if (usuario is null || !_passwordHasher.VerifyPassword(senha, usuario.SenhaHash))
                throw new Exception("Email ou senha inválidos");

            return _tokenService.GerarToken(usuario.Id, usuario.Nome, usuario.Email, usuario.Role, usuario.CadastradoEm);
        }

        public async Task<UsuarioResponse> GetMeAsync()
        {
            var usuario = await _repo.GetMe();

            if (usuario is null)
                throw new Exception("Usuário não encontrado");

            return new UsuarioResponse
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Cpf = usuario.Cpf,
                Role = usuario.Role,
                CadastradoEm = usuario.CadastradoEm,
                Ativo = usuario.Ativo
            };
        }

        public async Task<bool> CadastrarDoadorAsync(CadastroDoadorRequest request)
        {
            var cpf = request.Cpf.OnlyDigits();

            if (!cpf.IsValidCpf())
                throw new Exception("CPF inválido");

            if (await _repo.EmailExists(request.Email))
                throw new Exception("Email já cadastrado no sistema");

            if (await _repo.CpfExists(cpf))
                throw new Exception("CPF já cadastrado no sistema");

            var doador = new Usuario
            {
                Nome = request.NomeCompleto,
                Email = request.Email,
                Cpf = cpf,
                SenhaHash = _passwordHasher.HashPassword(request.Senha),
                Role = Domain.ERole.Doador
            };

            return await _repo.Create(doador);
        }
    }
}
