using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vitalitas.Backend.API.Services.JwtService;
using static Application.DTOs.UsuarioRQ;
using static Application.DTOs.UsuarioRS; // Ajustado de UsuarioRP para UsuarioRS
using Application.Interfaces;
using System;

namespace API.Controllers
{
    [ApiController]
    [Route("vitalitas/user")]
    public class UserController : ControllerBase
    {
        private readonly IUsuarioUseCase _usuarioUseCase;

        public UserController(IUsuarioUseCase usuarioUseCase)
        {
            _usuarioUseCase = usuarioUseCase;
        }

        [HttpGet("test")]
        [Authorize]
        public IActionResult Test()
        {
            var idUsuario = User.FindFirst("IdUsuario")?.Value ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var tipoUsuario = User.FindFirst("TipoUsuario")?.Value;
            var role = User.FindFirst("Role")?.Value ?? User.FindFirst(ClaimTypes.Role)?.Value;

            return Ok(new
            {
                message = "Token valido",
                success = true,
                IdUsuario = idUsuario,
                TipoUsuario = tipoUsuario,
                Role = role
            });
        }

        [HttpGet("test-admin")]
        [Authorize(Roles = "Administrador")]
        [ApiExplorerSettings(GroupName = "Administrativo")]
        public IActionResult TestAdmin()
        {
            var tipoUsuario = User.FindFirst("TipoUsuario")?.Value;
            var idUsuario = User.FindFirst("IdUsuario")?.Value ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = User.FindFirst("Role")?.Value ?? User.FindFirst(ClaimTypes.Role)?.Value;

            return Ok(new
            {
                message = "Acesso autorizado para Administrador",
                success = true,
                IdUsuario = idUsuario,
                TipoUsuario = tipoUsuario,
                Role = role
            });
        }

        [HttpGet("test-token")]
        [AllowAnonymous]
        public IActionResult TestToken([FromServices] IJwtService jwt)
        {
            var token = jwt.GenerateToken("1", "Administrador");
            return Ok(new { token });
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult<LoginResponse> Login([FromBody] LoginRequest login)
        {
            try
            {
                var response = _usuarioUseCase.Login(login.Email, login.Senha);
                return Ok(response);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "Credenciais inválidas" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", detalhe = ex.Message });
            }
        }


        [HttpPut("trocar-senha")]
        public ActionResult<TrocarSenhaResponse> TrocarSenha([FromBody] TrocarSenhaRequest reset)
        {
            try
            {
                var response = _usuarioUseCase.TrocarSenha(reset.IdUsuario, reset.NovaSenha);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", detalhe = ex.Message });
            }
        }

        [HttpPost]
        [ApiExplorerSettings(GroupName = "Administrativo")]
        public ActionResult<CriarUsuarioResponse> CriarUsuario(CriarUsuarioRequest user)
        {

            try
            {
                var response = _usuarioUseCase.CriarUsuario(user.Nome, user.Email, user.Senha, user.Quadra, user.Rua, user.Bairro, user.Cidade, user.Estado, user.Cep, user.DataNascimento, user.Cpf, user.TipoUsuario);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", detalhe = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        [HttpPut("atualizar-dados")]
        public ActionResult<AtualizarDadosResponse> AtualizarDados([FromBody] AtualizarDadosRequest request)
        {
            try
            {
                var response = _usuarioUseCase.AtualizarDados(request.IdUsuario, request.Valor, request.Atributo);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = "Atributo inválido", detalhe = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", detalhe = ex.Message });
            }
        }

        /*[HttpPut("desativar")]
        public ActionResult<DesativarResponse> Desativar([FromBody] DesativarRequest request)
        {
            try
            {
                var response = _usuarioUseCase.Desativar(request.IdUsuario);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", detalhe = ex.Message });
            }
        }

        [HttpPut("ativar")]
        public ActionResult<AtivarResponse> Ativar([FromBody] AtivarRequest request)
        {
            try
            {
                var response = _usuarioUseCase.Ativar(request.IdUsuario);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", detalhe = ex.Message });
            }
        }*/

        [HttpGet("{id}/logs")]
        public ActionResult<ObterLogsResponse> ObterLogs([FromRoute] Guid id)
        {
            try
            {
                var response = _usuarioUseCase.ObterLogs(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", detalhe = ex.Message });
            }
        }
    }
}


