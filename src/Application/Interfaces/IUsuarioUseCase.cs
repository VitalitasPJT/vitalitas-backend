using Domain.Entities; // Supondo que LogAtividade esteja no Domínio, ou ajuste para o seu DTO
using Domain.Enums;
using System;
using System.Collections.Generic;
using static Application.DTOs.UsuarioRS;

namespace Application.Interfaces
{
    public interface IUsuarioUseCase
    {
        LoginResponse Login(string email, string senha);

        TrocarSenhaResponse TrocarSenha(Guid idusuario, string novasenha);

        CriarUsuarioResponse CriarUsuario(string nome, string email, string senha, string quadra, string rua, string bairro, string cidade, string estado, string cep, DateOnly dataNascimento, string cpf, TipoUsuario tipoUsuario);

        AtualizarDadosResponse AtualizarDados(Guid idusuario, dynamic valor, string atributo);

        /*DesativarResponse Desativar(Guid idusuario);

        AtivarResponse Ativar(Guid idusuario);*/

        ObterLogsResponse ObterLogs(Guid idusuario);

        AdicionarLogResponse AdicionarLog(Guid idusuario, LogAtividade log);
    }
}