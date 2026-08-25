using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace UsersApi.Domain.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    public class Usuario
    {
        [Key] public int Id { get; set; }
        [Required][MaxLength(150)] public string Nome { get; set; }
        [Required][EmailAddress][MaxLength(150)] public string Email { get; set; }
        [MaxLength(11)] public string Cpf { get; set; }
        [Required][MaxLength(300)] public string SenhaHash { get; set; }
        [Required] public ERole Role { get; set; } = ERole.Doador;
        [Required] public DateTime CadastradoEm { get; set; } = DateTime.UtcNow;
        [Required] public bool Ativo { get; set; } = true;
    }
}
