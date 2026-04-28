using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IInstrutorRepository
    {
        List<Instrutor> ListarInstrutores(Guid idacademia);
        Guid CriarInstrutor(Instrutor instrutor);
        List<Guid> ListarAgenda(Guid idinstrutor);
    }
}
