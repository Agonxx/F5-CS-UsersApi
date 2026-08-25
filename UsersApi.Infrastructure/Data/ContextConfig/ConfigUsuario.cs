using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsersApi.Domain;
using UsersApi.Domain.Entities;

namespace UsersApi.Infrastructure.Data.ContextConfig
{
    public class ConfigUsuario : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasIndex(u => u.Cpf).IsUnique().HasFilter("[Cpf] IS NOT NULL");

            builder.HasData(new Usuario
            {
                Id = 1,
                Nome = "Gestor Esperança Solidária",
                Email = "gestor@esperancasolidaria.org",
                Cpf = null,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword("Gestor@123"),
                Role = ERole.GestorONG,
                CadastradoEm = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                Ativo = true
            });
        }
    }
}
