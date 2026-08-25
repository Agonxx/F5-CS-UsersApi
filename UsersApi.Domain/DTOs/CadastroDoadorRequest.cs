using System.ComponentModel.DataAnnotations;

namespace UsersApi.Domain.DTOs
{
    public class CadastroDoadorRequest
    {
        [Required][MaxLength(150)] public string NomeCompleto { get; set; }
        [Required][EmailAddress][MaxLength(150)] public string Email { get; set; }
        [Required] public string Cpf { get; set; }
        [Required][MinLength(6)] public string Senha { get; set; }
    }
}
