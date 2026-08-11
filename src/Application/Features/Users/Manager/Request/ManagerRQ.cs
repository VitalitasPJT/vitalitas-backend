using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.Features.Users.Manager.Request
{
    public class ManagerRQ
    {
        // Sem TipoUsuario aqui: fonte única de verdade é o discriminador do bloco
        // Perfil (CreatePerfilRequest.tipoUsuario), pra evitar os dois blocos do
        // request dizendo tipos diferentes. Ver CreateUserWithProfileRequest.
        public class CreateUserRequest
        {
            public Guid IdAcademia { get; set; }
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Senha { get; set; }
            public DateOnly DataNascimento { get; set; }
            public string Cpf { get; set; }
            public string Quadra { get; set; }
            public string Rua { get; set; }
            public string Bairro { get; set; }
            public string Cidade { get; set; }
            public string Estado { get; set; }
            public string Cep { get; set; }
        }

        // Discriminador polimórfico nativo do System.Text.Json: o campo "tipoUsuario"
        // dentro do JSON de Perfil decide, no model binding, qual subtipo é
        // desserializado. Requisição com "tipoUsuario" ausente/desconhecido já
        // falha o binding com 400 estruturado antes de chegar na action.
        [JsonPolymorphic(TypeDiscriminatorPropertyName = "tipoUsuario")]
        [JsonDerivedType(typeof(CreateMemberProfileRequest), "Aluno")]
        [JsonDerivedType(typeof(CreateInstructorProfileRequest), "Instrutor")]
        [JsonDerivedType(typeof(CreateEmployeeProfileRequest), "Administrador")]
        public abstract class CreatePerfilRequest
        {
        }

        public class CreateMemberProfileRequest : CreatePerfilRequest
        {
            public Guid IdInstrutor { get; set; }
            public Guid IdContrato { get; set; }
            public required string Objetivo { get; set; }
        }

        public class CreateInstructorProfileRequest : CreatePerfilRequest
        {
            public required string CREF { get; set; }
        }

        public class CreateEmployeeProfileRequest : CreatePerfilRequest
        {
            public Role Cargo { get; set; }
        }

        public class CreateUserWithProfileRequest
        {
            public required CreateUserRequest Usuario { get; set; }
            public required CreatePerfilRequest Perfil { get; set; }
        }
    }
}