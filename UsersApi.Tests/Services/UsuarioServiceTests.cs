using Moq;
using UsersApi.Application.Services;
using UsersApi.Domain;
using UsersApi.Domain.DTOs;
using UsersApi.Domain.Entities;
using UsersApi.Domain.Interfaces.Repositories;
using UsersApi.Domain.Interfaces.Services;
using UsersApi.Domain.Interfaces.Utils;

namespace UsersApi.Tests.Services
{
    public class UsuarioServiceTests
    {
        private readonly Mock<IUsuarioRepository> _repoMock;
        private readonly Mock<IPasswordHasher> _passwordHasherMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly UsuarioService _service;

        public UsuarioServiceTests()
        {
            _repoMock = new Mock<IUsuarioRepository>();
            _passwordHasherMock = new Mock<IPasswordHasher>();
            _tokenServiceMock = new Mock<ITokenService>();
            _service = new UsuarioService(_repoMock.Object, _passwordHasherMock.Object, _tokenServiceMock.Object);
        }

        [Fact]
        public async Task AuthAsync_DeveRetornarToken_QuandoCredenciaisValidas()
        {
            var email = "gestor@esperancasolidaria.org";
            var senha = "Gestor@123";
            var tokenEsperado = "jwt-token-123";
            var usuario = new Usuario
            {
                Id = 1,
                Nome = "Gestor",
                Email = email,
                SenhaHash = "hash-armazenado",
                Role = ERole.GestorONG,
                CadastradoEm = DateTime.UtcNow
            };

            _repoMock.Setup(r => r.GetByEmail(email)).ReturnsAsync(usuario);
            _passwordHasherMock.Setup(p => p.VerifyPassword(senha, usuario.SenhaHash)).Returns(true);
            _tokenServiceMock.Setup(t => t.GerarToken(usuario.Id, usuario.Nome, usuario.Email, usuario.Role, usuario.CadastradoEm)).Returns(tokenEsperado);

            var resultado = await _service.AuthAsync(email, senha);

            Assert.Equal(tokenEsperado, resultado);
        }

        [Fact]
        public async Task AuthAsync_DeveLancarExcecao_QuandoSenhaInvalida()
        {
            var email = "gestor@esperancasolidaria.org";
            var usuario = new Usuario { Id = 1, Email = email, SenhaHash = "hash-armazenado" };

            _repoMock.Setup(r => r.GetByEmail(email)).ReturnsAsync(usuario);
            _passwordHasherMock.Setup(p => p.VerifyPassword("errada", usuario.SenhaHash)).Returns(false);

            await Assert.ThrowsAsync<Exception>(() => _service.AuthAsync(email, "errada"));
        }

        [Fact]
        public async Task AuthAsync_DeveLancarExcecao_QuandoEmailNaoExiste()
        {
            _repoMock.Setup(r => r.GetByEmail(It.IsAny<string>())).ReturnsAsync((Usuario)null);

            await Assert.ThrowsAsync<Exception>(() => _service.AuthAsync("naoexiste@x.com", "qualquer"));
        }

        [Fact]
        public async Task CadastrarDoadorAsync_DeveHashearSenha_ECriarDoador()
        {
            var request = new CadastroDoadorRequest
            {
                NomeCompleto = "Maria Doadora",
                Email = "maria@doadora.com",
                Cpf = "111.444.777-35",
                Senha = "senha123"
            };
            var senhaHash = "hash-gerado";

            _repoMock.Setup(r => r.EmailExists(request.Email)).ReturnsAsync(false);
            _repoMock.Setup(r => r.CpfExists("11144477735")).ReturnsAsync(false);
            _passwordHasherMock.Setup(p => p.HashPassword(request.Senha)).Returns(senhaHash);
            _repoMock.Setup(r => r.Create(It.Is<Usuario>(u =>
                u.Email == request.Email &&
                u.Cpf == "11144477735" &&
                u.SenhaHash == senhaHash &&
                u.Role == ERole.Doador))).ReturnsAsync(true);

            var resultado = await _service.CadastrarDoadorAsync(request);

            Assert.True(resultado);
        }

        [Fact]
        public async Task CadastrarDoadorAsync_DeveLancarExcecao_QuandoCpfInvalido()
        {
            var request = new CadastroDoadorRequest
            {
                NomeCompleto = "Doador Invalido",
                Email = "novo@doador.com",
                Cpf = "111.111.111-11",
                Senha = "senha123"
            };

            var ex = await Assert.ThrowsAsync<Exception>(() => _service.CadastrarDoadorAsync(request));
            Assert.Equal("CPF inválido", ex.Message);
        }

        [Fact]
        public async Task CadastrarDoadorAsync_DeveLancarExcecao_QuandoEmailJaCadastrado()
        {
            var request = new CadastroDoadorRequest
            {
                NomeCompleto = "Duplicado",
                Email = "existente@doador.com",
                Cpf = "111.444.777-35",
                Senha = "senha123"
            };

            _repoMock.Setup(r => r.EmailExists(request.Email)).ReturnsAsync(true);

            var ex = await Assert.ThrowsAsync<Exception>(() => _service.CadastrarDoadorAsync(request));
            Assert.Equal("Email já cadastrado no sistema", ex.Message);
        }

        [Fact]
        public async Task GetMeAsync_DeveRetornarUsuarioLogado()
        {
            var usuarioLogado = new Usuario { Id = 2, Nome = "Doador", Email = "doador@x.com", Role = ERole.Doador };

            _repoMock.Setup(r => r.GetMe()).ReturnsAsync(usuarioLogado);

            var resultado = await _service.GetMeAsync();

            Assert.NotNull(resultado);
            Assert.Equal(usuarioLogado.Id, resultado.Id);
        }
    }
}
