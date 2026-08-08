using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Application.Features.Records.MedicalRecord.Response.MedicalRecordRP;
using Application.Features.Records.MedicalRecord.Constructor;

namespace Application.Features.Records.MedicalRecord.Interfaces
{
    public interface IMedicalRecordUseCase
    {
        CreateMedicalRecordResponse CriarFichaMedica(ConstructorMedicalRecord fichaMedica);
    }
}