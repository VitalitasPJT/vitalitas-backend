using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Compartilhado;

namespace Application.Fichas.FichaMedica.Response
{
    public class FichaMedicaRP
    {
        public class CriarFichaMedicaResponse
        {
            private Guid idFicha;

            public CriarFichaMedicaResponse(Guid idFicha, StatusHTTP status)
            {
                this.idFicha = idFicha;
                Status = status;
            }

            public Guid IdFichaMedica { get; set; }
            public StatusHTTP Status { get; set; }
        }
    }
}