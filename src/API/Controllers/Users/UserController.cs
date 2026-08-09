using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using API.Services;
using static Application.Features.Users.Common.Request.UserRQ;
using static Application.Features.Users.Common.Response.UserRS;
using Application.Features.Users.Common.Interfaces;
using Application.Token.Interfaces;
using System;

namespace API.Controllers.Users
{
    [ApiController]
    [Route("usuario")]
    public class UserController : ControllerBase
    {
        private readonly IUserUseCase _usuarioUseCase;
        private readonly IRefreshTokenUseCase _refreshTokenUseCase;

        public UserController(IUserUseCase usuarioUseCase, IRefreshTokenUseCase refreshTokenUseCase)
        {
            _usuarioUseCase = usuarioUseCase;
            _refreshTokenUseCase = refreshTokenUseCase;
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


        [HttpPost("refresh")]
        [AllowAnonymous]
        public ActionResult<RefreshResponse> Refresh([FromBody] RefreshRequest request)
        {
            try
            {
                var response = _refreshTokenUseCase.Refresh(request.AccessToken, request.RefreshToken);
                return Ok(response);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "Token inválido ou expirado" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", detalhe = ex.Message });
            }
        }

        [HttpGet("obter-tipo-usuario/{id}")]
        [Authorize]
        public ActionResult<GetUserTypeResponse> ObterTipoUsuario([FromRoute] Guid id)
        {
            var role = User.FindFirst("Role")?.Value;
            if (role != "Gestor" && role != "Administrador")
            {
                var jwtId = User.FindFirst("IdUsuario")?.Value;
                if (id.ToString() != jwtId)
                    return Forbid();
            }

            try
            {
                var response = _usuarioUseCase.ObterTipoUsuario(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", detalhe = ex.Message });
            }
        }
    }
}
