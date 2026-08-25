using System.ComponentModel.DataAnnotations;

namespace UsersApi.Domain.DTOs
{
    public class LoginRequest
    {
        [Required][EmailAddress] public string Email { get; set; }
        [Required] public string Senha { get; set; }
    }
}
