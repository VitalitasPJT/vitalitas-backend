using System;
using Application.Compartilhado;

namespace Application.Usuarios.Gestor.Response
{
    public class CriarFuncionarioResponse
    {
        public CriarFuncionarioResponse(Guid idFuncionario, StatusHTTP statusHTTP)
        {
            IdFuncionario = idFuncionario;
            Status = statusHTTP;
        }

        public StatusHTTP Status { get; set; }
        public Guid IdFuncionario { get; set; }
    }
}
