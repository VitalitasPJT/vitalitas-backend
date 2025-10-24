using Microsoft.AspNetCore.Mvc;
using Vitalitas.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

[ApiController]
[Route("vitalitas/fichas")]
public class FichaController : ControllerBase
{
    private readonly Contexto _context;

    public FichaController(Contexto context)
    {
        _context = context;
    }

    [HttpPost]
    public ActionResult<Responser<FichaDeTreino>> PostFicha([FromBody] FichaDeTreino fichaDeTreino)
    {

        _context.FichasDeTreinos.Add(fichaDeTreino);
        _context.SaveChanges();

        return Ok(new Responser<FichaDeTreino>("Ficha de treinos criada com sucesso", true, fichaDeTreino));
    }

    [HttpPost("treino")]
    public ActionResult<Responser<Treino>> PostTreino([FromBody] Treino treino)
    {
        //gera id aqui xxxxx)
        _context.Treinos.Add(treino);
        _context.SaveChanges();

        return Ok(new Responser<Treino>( "Treino criado com sucesso", true, treino));
    }

    [HttpPost("treino/exercicio")]
    public ActionResult<Responser<TreinoExercicio>> PostExercicio([FromBody] TreinoExercicio treinoExercicio)
    {
        //gera id aqui (pppp)
        _context.TreinoExercicios.Add(treinoExercicio);
        _context.SaveChanges();

        return Ok(new Responser<TreinoExercicio>("Exercicio salvo com sucesso", true, treinoExercicio));
    }

    [HttpGet]
    public async Task<ActionResult<Responser<List<FichaDeTreino>>>> GetFicha([FromQuery] string aluno)
    {
        var fichas = await (
            from u in _context.FichasDeTreinos
            where u.Id_Aluno == aluno
            select new FichaDeTreino
            {
                Id_Ficha = u.Id_Ficha,
                Id_Aluno = u.Id_Aluno,
                Responsavel = u.Responsavel,
                Data_Criacao = u.Data_Criacao,
                Data_Validade = u.Data_Validade,
                Nome = u.Nome,
                Observacoes = u.Observacoes

            }).ToListAsync();

        return Ok(new Responser<List<FichaDeTreino>>( "Listagem feita com sucesso", true, fichas));
    }

    [HttpGet("treino")]
    public async Task<ActionResult<Responser<List<Treino>>>> GetTreino([FromQuery] string idFicha)
    {
        var treino = await (
            from u in _context.Treinos
            where u.Id_Ficha_Treino == idFicha
            select new Treino
            {
                Id_Ficha_Treino = u.Id_Ficha_Treino, 
                Id_Treino = u.Id_Treino,
                Nome = u.Nome
            }).ToListAsync();

        return Ok(new Responser<List<Treino>>("Listagem feita com sucesso", true, treino));
    }

    [HttpGet("treino/exercicio")]
    public async Task<ActionResult<Responser<List<TreinoExercicio>>>> GetExercicio([FromQuery] string idTreino)
    {
        var exercicio = await (
            from u in _context.TreinoExercicios
            where u.Id_Treino == idTreino
            select new TreinoExercicio
            {
                Id_Treino = u.Id_Treino,
                Id_Exercicio = u.Id_Exercicio,
                Series = u.Series,
                Repeticoes = u.Repeticoes,
                Aparelho = u.Aparelho,
                Nome = u.Nome,
                Musculo = u.Musculo

            }).ToListAsync();

        return Ok(new Responser<List<TreinoExercicio>>("Listagem feita com sucesso", true, exercicio));
    }
}
