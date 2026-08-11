using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using static Application.Features.Users.Manager.Request.ManagerRQ;

namespace API.Swagger
{
    // CreatePerfilRequest é abstrata e sem propriedades próprias (o discriminador
    // polimórfico "TipoUsuario" e os campos de cada perfil só existem nas classes
    // derivadas) — sem isso, o Swagger mostra "Perfil": {} porque não tem o que
    // refletir na classe base. Os exemplos completos (Aluno/Instrutor/Administrador)
    // ficam em CriarUsuarioExamplesOperationFilter; aqui só a documentação textual
    // do formato, como fallback pra quem olhar o schema fora do dropdown de exemplos.
    public class PerfilRequestExampleSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type != typeof(CreatePerfilRequest))
                return;

            schema.Description = "Discriminado pelo campo \"TipoUsuario\". Formatos possíveis:\n\n"
                + "- Aluno: { \"TipoUsuario\": \"Aluno\", \"IdInstrutor\": \"guid\", \"IdContrato\": \"guid\", \"Objetivo\": \"string\" }\n"
                + "- Instrutor: { \"TipoUsuario\": \"Instrutor\", \"CREF\": \"string\" }\n"
                + "- Administrador: { \"TipoUsuario\": \"Administrador\", \"Cargo\": number } "
                + "(1=Recepcionista, 2=Personal, 3=Gerente, 4=Limpeza, 5=Outros — sem conversor de enum registrado, trafega como número, não como texto)\n\n"
                + "Veja o seletor de exemplos acima pra ver o corpo completo (Usuario + Perfil) de cada tipo.";
        }
    }
}
