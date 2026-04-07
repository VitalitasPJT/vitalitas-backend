using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.ValueObjects;
using static Application.DTOs.UsuarioRP;

namespace Application.Interfaces
{
    public interface IUsuarioUseCase
    {
        LoginResponse Login(string email, string senha);
        TrocarSenhaResponse TrocarSenha(Guid idusuario, string novasenha);
        CriarUsuarioResponse CriarUsuario(string nome, string email, string senha, string quadra, string rua, string bairro, string cidade, string estado, string cep, DateOnly dataNascimento, string cpf, TipoUsuario tipoUsuario);
    }
}