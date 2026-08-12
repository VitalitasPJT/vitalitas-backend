using System;
using System.Collections.Generic;
using Application.Shared;
using Domain.Features.Shared.Entities;
using Domain.Enums;

namespace Application.Features.Users.Common.Response
{
    public class UserRS
    {
        public class LoginResponse
        {
            public UserType TipoUsuario { get; set; }
            public Guid IdUsuario { get; set; }
            public Guid IdAcademia { get; set; }
            public bool Flag { get; set; }
            public string? Token { get; set; }
            public string? RefreshToken { get; set; }
            public HttpStatus Status { get; set; }
            public LoginResponse(UserType tipoUsuario, Guid idUsuario, Guid idAcademia, bool flag, HttpStatus status)
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
            public HttpStatus Status { get; set; }

            public RefreshResponse(string accessToken, string refreshToken, HttpStatus status)
            {
                AccessToken = accessToken;
                RefreshToken = refreshToken;
                Status = status;
            }
        }

        public class AddLogResponse
        {
            public Domain.Features.Shared.Entities.ActivityLog logResgistrado { get; set; }
            public HttpStatus Status { get; set; }

            public AddLogResponse(Domain.Features.Shared.Entities.ActivityLog logResgistrado, HttpStatus status)
            {
                this.logResgistrado = logResgistrado;
                Status = status;
            }
        }

        public class GetUserTypeResponse
        {
            public UserType TipoUsuario { get; set; }
            public required HttpStatus Status { get; set; }
        }

    }
}
