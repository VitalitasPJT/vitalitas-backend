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

            public LoginResponse(TipoUsuario tipoUsuario, Guid idUsuario, bool flag, StatusHTTP status)
            {
                TipoUsuario = tipoUsuario;
                IdUsuario = idUsuario;
                this.flag = flag;
                this.status = status;
            }

            public bool Sucesso { get; set; }
            public string Mensagem { get; set; }

            // Dados úteis para o Front-end ao logar
            public Guid IdUsuario { get; set; }
            public string Nome { get; set; }
            public string Email { get; set; }
            public TipoUsuario TipoUsuario { get; set; }

            // Espaço reservado para quando você implementar autenticação JWT
            public string Token { get; set; }
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
            public Guid IdUsuario { get; set; } // Retorna o ID gerado no banco
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

        // --- DTO Auxiliar para listar os logs de forma limpa ---

        public class LogDto
        {
            public Guid IdLog { get; set; }
            public string Acao { get; set; }
            public DateTime DataHora { get; set; }
        }
    }
}