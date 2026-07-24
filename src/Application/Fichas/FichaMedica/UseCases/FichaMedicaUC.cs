using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Compartilhado;
using Application.Fichas.FichaMedica.Response;
using Application.Fichas.FichaMedica.Interfaces;
using Domain.Features.Fichas.FichaMedica.Entities;
using Domain.Features.Fichas.FichaMedica.Interfaces;
using static Application.Fichas.FichaMedica.Response.FichaMedicaRP;
using Application.Fichas.FichaMedica.Constructor;

namespace Application.Fichas.FichaMedica.UseCases
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
            var novaFichaMedica = new Domain.Features.Fichas.FichaMedica.Entities.FichaMedica(fichaMedica.IdFicha, fichaMedica.IdAluno, fichaMedica.Alergia, fichaMedica.Restricao, fichaMedica.Lesao, fichaMedica.Cirurgia, fichaMedica.ProblemaSaude, fichaMedica.UsoMedicamento);
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