using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Application.DTOs
{
    public class UsuarioRS
    {
        public class LoginResponse
        {
            private bool flag;
            private StatusHTTP status;
            public TipoUsuario TipoUsuario { get; set; }
            public Guid IdUsuario { get; set;}
            public bool Flag {get; set;}
            public string? Token { get; set; }
            public StatusHTTP Status { get; set;}
            public LoginResponse(TipoUsuario tipoUsuario, Guid idUsuario, bool flag, StatusHTTP status)
            {
                this.TipoUsuario = tipoUsuario;
                this.IdUsuario = idUsuario;
                this.Flag = flag;
                this.Status = status;
                this.Token = null;
            }

            public bool Sucesso { get; set; }
            public string Mensagem { get; set; }
            public string Nome { get; set; }
            public string Email { get; set; }
        }

        public class TrocarSenhaResponse
        {
            private StatusHTTP status;

            public TrocarSenhaResponse(StatusHTTP status)
            {
                this.status = status;
            }

            public bool Sucesso { get; set; }
            public string Mensagem { get; set; }
        }

        public class CriarUsuarioResponse
        {
            private StatusHTTP statusHTTP;

            public CriarUsuarioResponse(Guid idUsuario, StatusHTTP statusHTTP)
            {
                IdUsuario = idUsuario;
                this.statusHTTP = statusHTTP;
            }

            public bool Sucesso { get; set; }
            public string Mensagem { get; set; }
            public Guid IdUsuario { get; set; } 
        }

        public class AtualizarDadosResponse
        {
            public bool Sucesso { get; set; }
            public string Mensagem { get; set; }
        }

        public class DesativarResponse
        {
            public bool Sucesso { get; set; }
            public string Mensagem { get; set; }
        }

        public class AtivarResponse
        {
            public bool Sucesso { get; set; }
            public string Mensagem { get; set; }
        }

        public class ObterLogsResponse
        {
            public bool Sucesso { get; set; }
            public string Mensagem { get; set; }
            public List<LogDto> Logs { get; set; } = new List<LogDto>();
        }

        public class AdicionarLogResponse
        {
            public bool Sucesso { get; set; }
            public string Mensagem { get; set; }
        }

        public class LogDto
        {
            public Guid IdLog { get; set; }
            public string Acao { get; set; }
            public DateTime DataHora { get; set; }
        }
    }
}
