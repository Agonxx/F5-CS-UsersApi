using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersApi.Domain.Constants;
using UsersApi.Domain.DTOs;
using UsersApi.Domain.Interfaces.Services;

namespace UsersApi.Api.Controllers
{
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IHttpContextAccessor httpContextAccessor,
                                    IUsuarioService service,
                                    InfoToken infoToken) : base(httpContextAccessor, infoToken)
        {
            _service = service;
        }

        [HttpPost(UsuarioApi.Auth)]
        [AllowAnonymous]
        public async Task<IActionResult> Auth([FromBody] LoginRequest login)
        {
            var token = await _service.AuthAsync(login.Email, login.Senha);
            return Ok(new { token });
        }

        [HttpPost(UsuarioApi.Cadastro)]
        [AllowAnonymous]
        public async Task<IActionResult> Cadastro([FromBody] CadastroDoadorRequest request)
        {
            var result = await _service.CadastrarDoadorAsync(request);
            return Ok(result);
        }

        [HttpGet(UsuarioApi.GetMe)]
        public async Task<IActionResult> GetMe()
        {
            var usuario = await _service.GetMeAsync();
            return Ok(usuario);
        }
    }
}
