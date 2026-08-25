using Microsoft.EntityFrameworkCore;
using UsersApi.Domain.DTOs;
using UsersApi.Domain.Entities;
using UsersApi.Domain.Interfaces.Repositories;
using UsersApi.Infrastructure.Data;

namespace UsersApi.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UsersDbContext _db;
        private readonly InfoToken _infoToken;

        public UsuarioRepository(UsersDbContext db, InfoToken infoToken)
        {
            _db = db;
            _infoToken = infoToken;
        }

        public async Task<Usuario> GetByEmail(string email)
        {
            return await _db.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Usuario> GetMe()
        {
            return await _db.Usuarios.FirstOrDefaultAsync(u => u.Id == _infoToken.Id);
        }

        public async Task<bool> Create(Usuario usuarioObj)
        {
            _db.Usuarios.Add(usuarioObj);
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _db.Usuarios.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> CpfExists(string cpf)
        {
            return await _db.Usuarios.AnyAsync(u => u.Cpf == cpf);
        }
    }
}
