using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Shared;
using Application.Features.Records.MedicalRecord.Response;
using Application.Features.Records.MedicalRecord.Interfaces;
using Domain.Features.Records.MedicalRecord.Entities;
using Domain.Features.Records.MedicalRecord.Interfaces;
using static Application.Features.Records.MedicalRecord.Response.MedicalRecordRP;
using Application.Features.Records.MedicalRecord.Constructor;

namespace Application.Features.Records.MedicalRecord.UseCases
{
    public class MedicalRecordUC : IMedicalRecordUseCase
    {
        private readonly IMedicalRecordRepository _fichaMedicaRepository;
        public MedicalRecordUC(IMedicalRecordRepository fichaMedicaRepository)
        {
            _fichaMedicaRepository = fichaMedicaRepository;
        }

        public CreateMedicalRecordResponse CriarFichaMedica(ConstructorMedicalRecord fichaMedica)
        {
            var novaFichaMedica = new Domain.Features.Records.MedicalRecord.Entities.MedicalRecord(fichaMedica.IdFicha, fichaMedica.IdAluno, fichaMedica.Alergia, fichaMedica.Restricao, fichaMedica.Lesao, fichaMedica.Cirurgia, fichaMedica.ProblemaSaude, fichaMedica.UsoMedicamento);
            var (success, idFicha) = _fichaMedicaRepository.CriarFichaMedica(novaFichaMedica);
            
            if (success)
            {
                var status = new HttpStatus(201, "Ficha médica criada com sucesso.");
                var response = new CreateMedicalRecordResponse(idFicha, status);
                return response;   
            }
            
            var errorStatus = new HttpStatus(500, "Erro ao criar ficha médica.");
            return new CreateMedicalRecordResponse(Guid.Empty, errorStatus);
        }
    }
}