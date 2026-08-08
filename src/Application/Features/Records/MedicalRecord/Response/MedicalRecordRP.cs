using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Shared;

namespace Application.Features.Records.MedicalRecord.Response
{
    public class MedicalRecordRP
    {
        public class CreateMedicalRecordResponse
        {
            private Guid idFicha;

            public CreateMedicalRecordResponse(Guid idFicha, HttpStatus status)
            {
                this.idFicha = idFicha;
                Status = status;
            }

            public Guid IdFichaMedica { get; set; }
            public HttpStatus Status { get; set; }
        }
    }
}