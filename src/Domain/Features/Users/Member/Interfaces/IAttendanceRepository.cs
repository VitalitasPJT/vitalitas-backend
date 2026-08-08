using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Features.Users.Member.Interfaces
{
    public interface IAttendanceRepository
    {
        dynamic RegistrarFrequencia(Guid idAluno, int tempoTreinoMinutos, DateTime data);
        dynamic ObterFrequenciaPorAluno(Guid idAluno);
        List<dynamic> ListarFrequenciasPorData(DateTime dataInicio, DateTime dataFim, Guid idAluno);
        dynamic AtualizarTempoTreino(Guid idFrequencia, int novoTempoTreinoMinutos);
        double CalcularTempoTotalTreino(Guid idAluno, DateTime dataInicio, DateTime dataFim);
        double CalcularMediaTempoTreino(Guid idAluno, DateTime dataInicio, DateTime dataFim);
    }
}