using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Users.Member.Entities
{
    public class Attendance
    {
        public Guid IdFrequencia { get; private set; }
        public Guid IdAluno { get; private set; }
        public int TempoTreinoMinutos { get; private set; }
        public DateTime Data {  get; private set; }

        public Attendance(Guid idAluno, int tempoTreinoMinutos, DateTime data)
        {
            IdFrequencia = Guid.NewGuid();
            IdAluno = idAluno;
            TempoTreinoMinutos = tempoTreinoMinutos;
            Data = data;
        }

        public Attendance() {}
    }
}
