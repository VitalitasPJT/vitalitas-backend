using Domain.Features.Usuarios.Common.Entities;
using Domain.Enums;

namespace Domain.Features.Usuarios.Funcionario.Interfaces
{
    public interface IFuncionarioRepository
    {
        List<Domain.Features.Usuarios.Funcionario.Entities.Funcionario> ListarFuncionarios(Guid idAcademia);
        Guid CriarUsuario(Guid idAcademia, string nome, string email, string senha, DateOnly dataNascimento, string cpf, TipoUsuario tipoUsuario, string quadra, string rua, string bairro, string cidade, string estado, string cep);
        Guid CriarAluno(Guid idInstrutor, Guid idUsuario, int idContrato, Guid idAcademia, string objetivo);
        dynamic AtualizarDados(Guid idUsuario, dynamic var, dynamic novoAtributo);
        dynamic Desativar(Guid idUsuario);
        dynamic Ativar(Guid idUsuario);
        List<Usuario> ListarUsuarios(Guid idAcademia);
        Usuario ListarUsuario(Guid idUsuario);
    }
}
