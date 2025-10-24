using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Vitalitas.Calculations;
using Vitalitas.Models;

namespace Vitalitas.Controllers
{
    [ApiController]
    [Route("vitalitas/avaliacao")]
    public class AvaliacaoController : ControllerBase
    {
        private readonly Contexto _context;
        private readonly CalculosFeminino _calculosFeminino;
        private readonly CalculosMasculino _calculosMasculino;

        public AvaliacaoController(Contexto context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult<Responser<Avaliacao>> PostAvaliacao([FromBody] Avaliacao avaliacao)
        {
            _context.Avaliacoes.Add(avaliacao);
            _context.SaveChanges();

            return Ok(new Responser<Avaliacao>("Avaliação inserida com sucesso", true, avaliacao));
        }

        [HttpPost("perimetro")]
        public ActionResult<Responser<Perimetro>> PostPerimetro([FromBody] Perimetro perimetro)
        {
            _context.Perimetros.Add(perimetro);
            _context.SaveChanges();

            return Ok(new Responser<Perimetro>("Perimetro inserido com sucesso", true, perimetro));
        }

        [HttpPost("cutaneas")]
        public ActionResult<Responser<Cutaneas>> PostCutaneas([FromBody] Cutaneas cutaneas)
        {
            _context.Cutaneass.Add(cutaneas);
            _context.SaveChanges();

            return Ok(new Responser<Cutaneas>("Perimetro inserido com sucesso", true, cutaneas));
        }

        [HttpPost("calcular")]
        public ActionResult<Responser<dynamic>> PostCalcular([FromBody] Calcular calcular)
        {
            var avaliacao = (from a in _context.Avaliacoes
                             where a.Id_Avaliacao == calcular.Id_Avaliacao
                             select new
                             {
                                 a.Peso,
                                 a.Altura,
                                 a.Idade
                             }).FirstOrDefault();

            var perimetro = (from u in _context.Perimetros
                             where u.Id_Avaliacao == calcular.Id_Avaliacao
                             select new
                             {
                                 u.Perna_E,
                                 u.Perna_D,
                                 u.Coxa_E,
                                 u.Coxa_D,
                                 u.Braco_D,
                                 u.Braco_E,
                                 u.Quadril,
                                 u.Abdomen,
                                 u.Deltoide,
                                 u.Cintura,
                                 u.Torax
                             }).FirstOrDefault();

            var cutanea = (from j in _context.Cutaneass
                           where j.Id_Avaliacao == calcular.Id_Avaliacao
                           select new
                           {
                               j.Tr,
                               j.Cx,
                               j.Si,
                               j.Ab,
                               j.Ax,
                               j.Pt,
                               j.Se,
                               j.Paturrilha,
                               j.Umero,
                               j.Femur
                           }).FirstOrDefault();


           switch (calcular.Sexo)
            {
                case "M":
                    if (avaliacao == null)
                        return BadRequest(new Responser<string>("Avaliação não encontrada.", false, null));

                    if (cutanea == null)
                        return BadRequest(new Responser<string>("Dobras cutâneas não encontradas.", false, null));
                    CalculosMasculino calculom = new CalculosMasculino(
                        avaliacao.Altura,
                        avaliacao.Peso,
                        cutanea.Tr,
                        cutanea.Cx,
                        cutanea.Si,
                        cutanea.Ab,
                        cutanea.Ax,
                        cutanea.Pt,
                        cutanea.Se,
                        avaliacao.Idade
                        );
                    Resultado resultm = new Resultado(
                        calcular.Id_Avaliacao, 
                        calculom.Imc, 
                        calculom.Soma_Das_Dobras, 
                        calculom.Densidade_Corporal, 
                        calculom.Percentual_De_Gordura, 
                        calculom.Massa_Gorda, 
                        calculom.Percentual_De_Massa_Magra,
                        calculom.Massa_Magra
                        );
                    _context.Resultado.Add(resultm);
                    _context.SaveChanges();
                    return Ok(new Responser<dynamic>("Inserido os calulos masculino com sucesso", true, resultm));
                case "F":
                    if (avaliacao == null)
                        return BadRequest(new Responser<string>("Avaliação não encontrada.", false, null));

                    if (cutanea == null)
                        return BadRequest(new Responser<string>("Dobras cutâneas não encontradas.", false, null));
                    CalculosFeminino calculof = new CalculosFeminino(
                        avaliacao.Altura,
                        avaliacao.Peso,
                        cutanea.Tr,
                        cutanea.Cx,
                        cutanea.Si,
                        cutanea.Ab,
                        cutanea.Ax,
                        cutanea.Pt,
                        cutanea.Se,
                        avaliacao.Idade
                        );
                    Resultado resultf = new Resultado(
                        calcular.Id_Avaliacao,
                        calculof.Imc,
                        calculof.Soma_Das_Dobras,
                        calculof.Densidade_Corporal,
                        calculof.Percentual_De_Gordura,
                        calculof.Massa_Gorda,
                        calculof.Percentual_De_Massa_Magra,
                        calculof.Massa_Magra
                        );
                    _context.Resultado.Add(resultf);
                    _context.SaveChanges();
                    return Ok(new Responser<dynamic>("Inserido os calulos femininos com sucesso", true, resultf));
                case "":
                    return BadRequest("sexo vazio");
            }
            return Ok(new Responser<dynamic>("", true, null));
        }

        [HttpGet]
        public async Task<ActionResult<Responser<List<dynamic>>>> GetAvaliacoes([FromQuery] string aluno)
        {
            var resultados = await (
                from u in _context.Resultado
                join a in _context.Avaliacoes on u.Id_Avaliacao equals a.Id_Avaliacao
                where a.Id_Aluno == aluno
                select new
                {
                    Id_Avaliacao = u.Id_Avaliacao,
                    Imc = u.Imc,
                    Soma_Das_Dobras = u.Soma_Das_Dobras,
                    Densidade_Corporal = u.Densidade_Corporal,
                    Percentual_De_Gordura = u.Percentual_De_Gordura,
                    Massa_Gorda = u.Massa_Gorda,
                    Percentual_De_Massa_Magra = u.Percentual_De_Massa_Magra,
                    Massa_Magra = u.Massa_Magra
                }
                ).ToListAsync();

            return Ok(new Responser<dynamic>("", true, resultados));
        }

        [HttpGet("ultimo")]
        public async Task<ActionResult<Responser<List<dynamic>>>> GetAvaliacaoRecente([FromQuery] string aluno)
        {
            var resultados = await (
                from u in _context.Resultado
                join a in _context.Avaliacoes on u.Id_Avaliacao equals a.Id_Avaliacao
                where a.Id_Aluno == aluno
                orderby a.Data descending
                select new
                {
                    Id_Avaliacao = u.Id_Avaliacao,
                    Imc = u.Imc,
                    Soma_Das_Dobras = u.Soma_Das_Dobras,
                    Densidade_Corporal = u.Densidade_Corporal,
                    Percentual_De_Gordura = u.Percentual_De_Gordura,
                    Massa_Gorda = u.Massa_Gorda,
                    Percentual_De_Massa_Magra = u.Percentual_De_Massa_Magra,
                    Massa_Magra = u.Massa_Magra
                }
                ).FirstOrDefaultAsync();

            return Ok(new Responser<dynamic>("", true, resultados));
        }
    }
}
