using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Features.Plans.License.Interfaces
{
    public interface ILicenseRepository
    {
        dynamic CriarLicenca(Guid idPlano, Monetary mensalidade, LicenseStatus status, LicenseType tipo, DateOnly dataFim, DateOnly dateAssinatura, string caminhoPdf);
        dynamic AtualizarMensalidade(Guid idLicenca, Monetary mensalidade);
        dynamic AtualizarDataFim(Guid idLicenca, DateOnly dataFim);
        dynamic AtualizarStatus(Guid idLicenca, LicenseStatus status);
        dynamic AtualziarCaminhoPdf(Guid idLicenca, string caminhoPdf);
        LicenseStatus ObterStatusLicenca(Guid idLicenca);
        Domain.Features.Plans.License.Entities.License ObterLicencaPlano(Guid idPlano);
        List<Domain.Features.Plans.License.Entities.License> ListarLicencasPorPlano(Guid idPlano);
        bool VerificarLicencaAtiva(Guid idPlano);
        dynamic GerarPdf(string caminhoPdf, Guid idLicenca);
    }
}