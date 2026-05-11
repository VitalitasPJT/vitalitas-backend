using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.DTOs.Response;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using DTOs.Constructor;
using static Application.DTOs.Response.FichaMedicaRP;
using static DTOs.Constructor.Constructor;

namespace Application.Services
{
    public class FichaMedicaUC : IFichaMedicaUseCase
    {
        private readonly IFichaMedicaRepository _fichaMedicaRepository;
        public FichaMedicaUC(IFichaMedicaRepository fichaMedicaRepository)
        {
            _fichaMedicaRepository = fichaMedicaRepository;
        }

        public CriarFichaMedicaResponse CriarFichaMedica(ConstructorFichaMedica fichaMedica)
        {
            var novaFichaMedica = new FichaMedica(fichaMedica.IdFicha, fichaMedica.IdAluno, fichaMedica.Alergia, fichaMedica.Restricao, fichaMedica.Lesao, fichaMedica.Cirurgia, fichaMedica.ProblemaSaude, fichaMedica.UsoMedicamento);
            var (success, idFicha) = _fichaMedicaRepository.CriarFichaMedica(novaFichaMedica);
            
            if (success)
            {
                var status = new StatusHTTP(201, "Ficha médica criada com sucesso.");
                var response = new CriarFichaMedicaResponse(idFicha, status);
                return response;   
            }
            
            var errorStatus = new StatusHTTP(500, "Erro ao criar ficha médica.");
            return new CriarFichaMedicaResponse(Guid.Empty, errorStatus);
        }
    }
}