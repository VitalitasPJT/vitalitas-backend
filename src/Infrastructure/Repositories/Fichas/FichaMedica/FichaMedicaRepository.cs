using Domain.Features.Fichas.FichaMedica.Interfaces;
using Infrastructure.Database.Context;

namespace Infrastructure.Repositories.Fichas.FichaMedica
{
    public class FichaMedicaRepository : IFichaMedicaRepository
    {
        private readonly VitalitasDbContext _context;

        public FichaMedicaRepository(VitalitasDbContext context)
        {
            _context = context;
        }

        public dynamic AtualizarFichaMedica(Domain.Features.Fichas.FichaMedica.Entities.FichaMedica novaFichaMedica)
        {
            throw new NotImplementedException();
        }

        public (bool, Guid) CriarFichaMedica(Domain.Features.Fichas.FichaMedica.Entities.FichaMedica fichaMedica)
        {
            _context.FichasMedicas.Add(fichaMedica);
            var rowsAffected = _context.SaveChanges();

            return rowsAffected > 0 ? (true, fichaMedica.IdFicha) : (false, Guid.Empty);
        }

        public Domain.Features.Fichas.FichaMedica.Entities.FichaMedica ListarFichaMedica(Guid idAluno)
        {
            throw new NotImplementedException();
        }
    }
}
