using System.Collections.Generic;
using API.Controllers.Users;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Swagger
{
    // POST /gestor/criar-usuario aceita um Perfil polimórfico (Aluno/Instrutor/
    // Administrador) que o Swagger sozinho não sabe exemplificar, porque o campo
    // é do tipo abstrato CreatePerfilRequest. Aqui a gente troca o exemplo único
    // do corpo da requisição por um dropdown com os três formatos completos —
    // é o que a equipe de frontend usa direto no "Try it out".
    public class CriarUsuarioExamplesOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.MethodInfo?.DeclaringType != typeof(ManagerController) ||
                context.MethodInfo.Name != nameof(ManagerController.CriarUsuario))
                return;

            if (operation.RequestBody == null ||
                !operation.RequestBody.Content.TryGetValue("application/json", out var mediaType))
                return;

            mediaType.Example = null;
            mediaType.Examples = new Dictionary<string, OpenApiExample>
            {
                ["Aluno"] = new OpenApiExample
                {
                    Summary = "Criar Aluno",
                    Description = "Gestor ou Administrador podem criar. Requer IdInstrutor/IdContrato de registros já existentes.",
                    Value = BuildBody(new OpenApiObject
                    {
                        ["TipoUsuario"] = new OpenApiString("Aluno"),
                        ["IdInstrutor"] = new OpenApiString("string"),
                        ["IdContrato"] = new OpenApiString("string"),
                        ["Objetivo"] = new OpenApiString("string")
                    })
                },
                ["Instrutor"] = new OpenApiExample
                {
                    Summary = "Criar Instrutor",
                    Description = "Só Gestor pode criar.",
                    Value = BuildBody(new OpenApiObject
                    {
                        ["TipoUsuario"] = new OpenApiString("Instrutor"),
                        ["CREF"] = new OpenApiString("string")
                    })
                },
                ["Administrador"] = new OpenApiExample
                {
                    Summary = "Criar Administrador",
                    Description = "Só Gestor pode criar. Cargo é número (Role): 1=Recepcionista, 2=Personal, 3=Gerente, 4=Limpeza, 5=Outros.",
                    Value = BuildBody(new OpenApiObject
                    {
                        ["TipoUsuario"] = new OpenApiString("Administrador"),
                        ["Cargo"] = new OpenApiString("int")
                    })
                }
            };
        }

        private static OpenApiObject BuildBody(OpenApiObject perfil) => new OpenApiObject
        {
            ["Usuario"] = new OpenApiObject
            {
                ["IdAcademia"] = new OpenApiString("string"),
                ["Nome"] = new OpenApiString("string"),
                ["Email"] = new OpenApiString("string"),
                ["Senha"] = new OpenApiString("string"),
                ["DataNascimento"] = new OpenApiString("string"),
                ["Cpf"] = new OpenApiString("string"),
                ["Quadra"] = new OpenApiString("string"),
                ["Rua"] = new OpenApiString("string"),
                ["Bairro"] = new OpenApiString("string"),
                ["Cidade"] = new OpenApiString("string"),
                ["Estado"] = new OpenApiString("string"),
                ["Cep"] = new OpenApiString("string")
            },
            ["Perfil"] = perfil
        };
    }
}
