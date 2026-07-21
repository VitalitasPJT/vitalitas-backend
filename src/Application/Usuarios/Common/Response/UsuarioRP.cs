using System;
using System.Collections.Generic;
using Application.Compartilhado;
using Domain.Features.Shared.Entities;
using Domain.Enums;

namespace Application.Usuarios.Common.Response
{
    public class UsuarioRS
    {
        public class LoginResponse
        {
            public TipoUsuario TipoUsuario { get; set; }
            public Guid IdUsuario { get; set; }
            public Guid IdAcademia { get; set; }
            public bool Flag { get; set; }
            public string? Token { get; set; }
            public string? RefreshToken { get; set; }
            public StatusHTTP Status { get; set; }
            public LoginResponse(TipoUsuario tipoUsuario, Guid idUsuario, Guid idAcademia, bool flag, StatusHTTP status)
            {
                this.TipoUsuario = tipoUsuario;
                this.IdUsuario = idUsuario;
                this.IdAcademia = idAcademia;
                this.Flag = flag;
                this.Status = status;
                this.Token = null;
                this.RefreshToken = null;
            }
        }

        public class RefreshResponse
        {
            public string AccessToken { get; set; }
            public string RefreshToken { get; set; }
            public StatusHTTP Status { get; set; }

            public RefreshResponse(string accessToken, string refreshToken, StatusHTTP status)
            {
                AccessToken = accessToken;
                RefreshToken = refreshToken;
                Status = status;
            }
        }

        public class AdicionarLogResponse
        {
            public LogAtividade logResgistrado { get; set; }
            public StatusHTTP Status { get; set; }

            public AdicionarLogResponse(LogAtividade logResgistrado, StatusHTTP status)
            {
                this.logResgistrado = logResgistrado;
                Status = status;
            }
        }

        public class ObterTipoUsuarioResponse
        {
            public TipoUsuario TipoUsuario { get; set; }
            public required StatusHTTP Status { get; set; }
        }

    }
}
