using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Features.Gym.Interfaces
{
    public interface IGymRepository
    {
        dynamic CriarAcademia(Guid idLicenca, Guid idGestor, Name nomeAcademia, CNPJ cnpj, string quadra, string rua, string bairro, string cidade, string estado, string cep, GymType tipoAcademia, Email emailInstitucional);
        dynamic AtualizarAcademia(Guid idAcademia, Guid idLicenca, Guid idGestor, Name nomeAcademia, CNPJ cnpj, string quadra, string rua, string bairro, string cidade, string estado, string cep, GymType tipoAcademia, Email emailInstitucional);
        dynamic AtualizarLicenca(Guid idAcademia, Guid idLicenca);
        dynamic AtualizarGestor(Guid idAcademia, Guid idGestor);
        Domain.Features.Gym.Entities.Gym ObterAcademia(Guid idAcademia);
        List<Domain.Features.Gym.Entities.Gym> ListarAcademias();
        List<dynamic> ListarALunos(Guid idacademia);
        dynamic DesativarAcademia(Guid idAcademia);
        dynamic AtivarAcademia(Guid idAcademia);
    }
}