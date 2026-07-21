using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Features.Planos.Licenca.Interfaces
{
    public interface ILicencaRepository
    {
        dynamic CriarLicenca(Guid idPlano, Monetario mensalidade, StatusLicenca status, TipoLicenca tipo, DateOnly dataFim, DateOnly dateAssinatura, string caminhoPdf);
        dynamic AtualizarMensalidade(Guid idLicenca, Monetario mensalidade);
        dynamic AtualizarDataFim(Guid idLicenca, DateOnly dataFim);
        dynamic AtualizarStatus(Guid idLicenca, StatusLicenca status);
        dynamic AtualziarCaminhoPdf(Guid idLicenca, string caminhoPdf);
        StatusLicenca ObterStatusLicenca(Guid idLicenca);
        Domain.Features.Planos.Licenca.Entities.Licenca ObterLicencaPlano(Guid idPlano);
        List<Domain.Features.Planos.Licenca.Entities.Licenca> ListarLicencasPorPlano(Guid idPlano);
        bool VerificarLicencaAtiva(Guid idPlano);
        dynamic GerarPdf(string caminhoPdf, Guid idLicenca);
    }
}