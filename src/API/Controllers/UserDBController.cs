using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Vitalitas.Backend.Application.DTOs;
using Vitalitas.Backend.Infrastructure.Persistence.Contexts;

namespace Vitalitas.Backend.API.Controllers
{
    [ApiController]
    [Route("vitalitas/user")]
    public class UserController : ControllerBase
    {
        private readonly Contexto _context;

        public UserController(Contexto context)
        {
            _context = context;
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok(new { message = "Hello World", success = true });
        }

        [HttpPost("login")]
        public ActionResult<LoginResponse> Login([FromBody] LoginRequest login)
        {
            var usuario = _context.Usuarios
                        .Where(u => u.Email == login.Email && u.Senha == login.Password)
                        .Select(u => new { u.TipoUsuario, u.IdUsuario }).FirstOrDefault();

            if (usuario != null)
            {
                Status status = new Status("Usuario encontrado", 200, true);
                LoginResponse response = new LoginResponse(usuario.TipoUsuario, usuario.IdUsuario, status);
                return Ok(response);
            }
            else
            {
                Status status = new Status("Usuario não encontrado", 404, false);
                LoginResponse response = new LoginResponse(null, 0, status);
                return BadRequest(response);
            }
        }

        /*
        [HttpGet]
        public async Task<ActionResult<Responser<List<Usuario>>>> Get()
        {
            var listadeusuarios = await (_context.Usuarios).ToListAsync();
            return Ok(new Responser<List<Usuario>>("Listagem de todos usuarios feito com sucesso", true, listadeusuarios));
        }


        [HttpGet("{id}")]
        public ActionResult<Responser<dynamic>> Get(string id)
        {
            var user = _context.Usuarios.Find(id);
            if (user == null)
                return NotFound(new Responser<dynamic>("Usuario nao encontrado com esse id", false, null));

            return Ok(new Responser<dynamic>("Usuario com o id encontrado", true, user));
        }


        [HttpGet("sexo")]
        public ActionResult<Responser<dynamic>> GetSexo([FromQuery] string id)
        {
            var user = (from u in _context.Usuarios
                        join j in _context.Alunos on u.IdUsuario equals j.Id_Usuario
                        where u.Id_Usuario == id
                        select new
                        {
                            u.Nome,
                            j.Sexo
                        }).FirstOrDefault();

            if (user == null)
                return NotFound(new Responser<dynamic>("Usuario nao encontrado com esse id", false, null));

            return Ok(new Responser<dynamic>("Sexo do aluno encontrado", true, user.Sexo));
        }



        [HttpPost("loginuser")]
        public ActionResult<LoginResponseUser> LoginUser([FromBody] LoginUser login)
        {
            var usuario = _context.Usuario
                .Where(u => u.Usuario == login.Usuario && u.Senha == login.Password)
                .Select(u => new { u.Tipo, u.Id })
                .FirstOrDefault();

            if (usuario != null)
            {
                var response = new LoginResponseUser
                {
                    Sucesso = "true",
                    Tipo = usuario.Tipo,
                    Mensagem = "Login efetuado",
                    Id = usuario.Id
                };

                return Ok(response);
            }
            else
            {
                return Unauthorized(new LoginResponseUser
                {
                    Sucesso = "false",
                    Tipo = null,
                    Mensagem = "Usuário ou senha inválidos",
                    Id = null,
                });
            }
        } AQUII PRE SELECT

        [HttpPost]
        public ActionResult<Responser<Usuario>> Post(Usuario user)
        {
            _context.Usuarios.Add(user);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new Responser<Usuario>("Usuario criado com sucesso", true, user));
        }

        [HttpPost("aluno")]
        public ActionResult<Responser<Aluno>> PostAluno([FromBody] Aluno aluno)
        {
            _context.Alunos.Add(aluno);
            _context.SaveChanges();

            return Ok(new Responser<Aluno>("Usuario do tipo ALUNO criado com sucesso", true, aluno));
        }

        [HttpPost("professor")]
        public ActionResult<Responser<Professor>> PostProfessor([FromBody] Professor professor)
        {
            _context.Professores.Add(professor);
            _context.SaveChanges();

            return Ok(new Responser<Professor>("Usuario do tipo PROFESSOR criado com sucesso", true, professor));
        }

        [HttpPost("administrador")]
        public ActionResult<Responser<Administrador>> PostAdministrador([FromBody] Administrador administrador)
        {
            _context.Administradores.Add(administrador);
            _context.SaveChanges();

            return Ok(new Responser<Administrador>("Usuario do tipo ADMINISTRADOR criado com sucesso", true, administrador));
        }

        [HttpGet("professores")]
        public async Task<ActionResult<Responser<List<ProfessorDados>>>> getProfessores()
        {
            var professores = await (
                from u in _context.Usuarios
                select new ProfessorDados
                {
                    Nome = u.Nome,
                    Id = u.IdUsuario,
                }
                ).ToListAsync();

            return Ok(new Responser<List<ProfessorDados>>("Listagem dos professores feito com sucesso", true, professores));
        }

        [HttpGet("alunos")]
        public async Task<ActionResult<Responser<List<object>>>> getAlunos([FromQuery] string prof)
        {
            var alunos = await (
                from u in _context.Usuarios
                join p in _context.Alunos on u.IdUsuario equals p.Id_Usuario
                where p.Responsavel == prof
                select new 
                {
                    Id = u.Id_Usuario,
                    Nome = u.Nome,
                    Email = u.Email,
                    Telefone = u.Telefone,

                    Status =  p.Status,
                    Data_Inscricao = p.Data_Inscricao,
                    Objetivo = p.Objetivo,
                    //Cpf = p.Cpf,
                    Data_Nascimento = p.Data_Nascimento,
                    Responsavel = p.Responsavel
                }
                ).ToListAsync();


            return Ok(new Responser<List<object>>("", true, alunos.Cast<object>().ToList()));
        }




        [HttpPut("{id}")]
        public IActionResult Put(string id, Usuario user)
        {
            if (id != user.IdUsuario)
                return BadRequest();

            _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _context.Usuarios.Find(id);
            if (user == null)
                return NotFound();

            _context.Usuarios.Remove(user);
            _context.SaveChanges();
            return NoContent();
        }
        */
    }
}