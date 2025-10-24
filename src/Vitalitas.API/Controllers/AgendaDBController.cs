using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Vitalitas.Models;

[ApiController]
[Route("vitalitas/agenda")]
public class AgendaController : ControllerBase
{
    private readonly Contexto _context;
    public AgendaController(Contexto context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<Responser<Agenda>>> PostAgenda([FromBody] Agenda agenda)
    {
        var existe = await _context.Agendas.FirstOrDefaultAsync(e => (e.Data == agenda.Data) && (e.Hora == agenda.Hora));
        if (existe == null)
        {
            _context.Agendas.Add(agenda);
            _context.SaveChanges();
        }
        else
        {
            return Conflict(new Responser<Agenda>("Já existe avaliação agendada para esse horario", false, null));
        }
        

        return Ok(new Responser<Agenda>("Agendado com sucesso", true, agenda));
    }

    [HttpGet]
    public async Task<ActionResult<Responser<List<Agenda>>>> GetAgenda([FromQuery] string idProf)
    {
        var agendas = await (
            from u in _context.Agendas
            where u.Id_Professor == idProf
            select new Agenda
            {
                Id_Agenda = u.Id_Agenda,
                Id_Aluno = u.Id_Aluno,
                Id_Professor = u.Id_Professor,
                Status = u.Status,
                Hora = u.Hora,
                Data = u.Data
            }
        ).ToListAsync();

        if (agendas == null || agendas.Count == 0)
        {
            return NotFound(new Responser<List<Agenda>>("Nenhuma agenda encontrada para este professor", false, null));
        }

        return Ok(new Responser<List<Agenda>>("Agendas carregadas com sucesso", true, agendas));
    }


}
