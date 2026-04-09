using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vitalitas.Backend.API.Services.JwtService;
using static Application.DTOs.UsuarioRQ;
using static Application.DTOs.UsuarioRP;
using LoginRequest = Application.DTOs.UsuarioRQ.LoginRequest;
using Application.Interfaces;
using System.Diagnostics;

namespace API.Controllers
{
    [ApiController]
    [Route("vitalitas/user")]
    public class UserController : ControllerBase
    {
        private readonly IUsuarioUseCase _usuarioUseCase;
        public UserController(IUsuarioUseCase usuarioUseCase)
        {
            _usuarioUseCase = usuarioUseCase;
        }

        [HttpGet("test")]
        [Authorize]
        public IActionResult Test()
        {
            return Ok(new { message = "Hello World", success = true });
        }

        [HttpGet("test-token")]
        [AllowAnonymous]
        public IActionResult TestToken([FromServices] IJwtService jwt)
        {
            var token = jwt.GenerateToken("1", "Administrador");
            return Ok(new { token });
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult<LoginResponse> Login([FromBody] LoginRequest login, [FromServices] IJwtService jwt)
        {
            try
            {
                var response = _usuarioUseCase.Login(login.Email, login.Senha);
                response.Token = jwt.GenerateToken(response.IdUsuario.ToString(), response.TipoUsuario.ToString());
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", detalhe = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        [HttpPut("trocar-senha")]
        public ActionResult<TrocarSenhaResponse> TrocarSenha([FromBody] TrocarSenhaRequest reset)
        {
            try
            {
                var response = _usuarioUseCase.TrocarSenha(reset.IdUsuario, reset.NovaSenha);
                return Ok(response);
            }
            catch
            {
                return StatusCode(500, new { message = "Erro interno do servidor" });
            }
            /*var usuario = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == reset.Id);

            if (usuario != null)
            {
                usuario.Senha = reset.NewPassword;
                usuario.SenhaFlag = true;
                _context.SaveChanges();
                Status status = new Status("Senha atualizada com sucesso", 200, true);
                PasswordResetResponse response = new PasswordResetResponse(status);
                return Ok(response);
            }
            else
            {
                Status status = new Status("Usuario não encontrado", 404, false);
                PasswordResetResponse response = new PasswordResetResponse(status);
                return BadRequest(response);
            }*/
        }

        [HttpPost]
        public ActionResult<CriarUsuarioResponse> CriarUsuario(CriarUsuarioRequest user)
        {
            try
            {
                var response = _usuarioUseCase.CriarUsuario(user.Nome, user.Email, user.Senha, user.Quadra, user.Rua, user.Bairro, user.Cidade, user.Estado, user.Cep, user.DataNascimento, user.Cpf, user.TipoUsuario);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", detalhe = ex.Message, StackTrace = ex.StackTrace });
            }
            /*_context.Usuarios.Add(user);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new Responser<Usuario>("Usuario criado com sucesso", true, user));*/
        }

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
