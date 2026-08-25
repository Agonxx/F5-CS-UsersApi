namespace UsersApi.Domain.DTOs
{
    public class UsuarioResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public ERole Role { get; set; }
        public DateTime CadastradoEm { get; set; }
        public bool Ativo { get; set; }
    }
}
