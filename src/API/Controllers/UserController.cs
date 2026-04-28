using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Test()
        {
            return Ok(new { message = "Hello World", success = true });
        }

        [HttpGet("test-token")]
        public IActionResult TestToken([FromServices] IJwtService jwt)
        {
            var token = jwt.GenerateToken("1", "Pedro");
            return Ok(new { token });
        }


        [HttpPost("login")]
        public ActionResult<LoginResponse> Login([FromBody] LoginRequest login)
        {
            try
            {
                var response = _usuarioUseCase.Login(login.Email, login.Senha);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", detalhe = ex.Message, StackTrace = ex.StackTrace });
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
        public ActionResult<CriarUsuarioResponse> CriarUsuario([FromBody] CriarUsuarioRequest user)
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