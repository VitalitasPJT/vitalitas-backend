using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Interfaces
{
    public interface IAcademiaRepository
    {
        dynamic CriarAcademia(Guid idLicenca, Guid idGestor, Nome nomeAcademia, CNPJ cnpj, string quadra, string rua, string bairro, string cidade, string estado, string cep, TipoAcademia tipoAcademia, Email emailInstitucional);
        dynamic AtualizarAcademia(Guid idAcademia, Guid idLicenca, Guid idGestor, Nome nomeAcademia, CNPJ cnpj, string quadra, string rua, string bairro, string cidade, string estado, string cep, TipoAcademia tipoAcademia, Email emailInstitucional);
        dynamic AtualizarLicenca(Guid idAcademia, Guid idLicenca);
        dynamic AtualizarGestor(Guid idAcademia, Guid idGestor);
        Academia ObterAcademia(Guid idAcademia);
        List<Academia> ListarAcademias();
        List<dynamic> ListarALunos(Guid idacademia);
        dynamic DesativarAcademia(Guid idAcademia);
        dynamic AtivarAcademia(Guid idAcademia);
    }
}