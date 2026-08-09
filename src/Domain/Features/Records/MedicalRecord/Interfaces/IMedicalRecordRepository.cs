using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Records.MedicalRecord.Interfaces
{
    public interface IMedicalRecordRepository
    {
        (bool, Guid) CriarFichaMedica(Domain.Features.Records.MedicalRecord.Entities.MedicalRecord fichaMedica);
        dynamic AtualizarFichaMedica(Domain.Features.Records.MedicalRecord.Entities.MedicalRecord novaFichaMedica);
        Domain.Features.Records.MedicalRecord.Entities.MedicalRecord ListarFichaMedica(Guid idAluno);
    }
}

