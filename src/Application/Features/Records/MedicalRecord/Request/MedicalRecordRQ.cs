using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Features.Records.MedicalRecord.Request
{
    public class MedicalRecordRQ
    {
        public class CreateMedicalRecordRequest
        {
            public Guid IdAluno { get; set; }
            public string Alergia { get; set; }
            public string Restricao { get; set; }
            public string Lesao { get; set; }
            public string Cirurgia { get; set; }
            public string ProblemaSaude { get; set; }
            public string UsoMedicamento { get; set; }
        }
    }
}