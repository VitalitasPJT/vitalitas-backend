using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Domain.Enums;
using Xunit;

namespace API.IntegrationTests
{
    // HU-03: Bloquear Acessos com Credenciais Inválidas — token válido, mas
    // perfil sem permissão pra rota (403, não 401 — a identidade é boa, falta
    // autorização).
    public class TokenPerfilIncorretoTests : IClassFixture<AuthPipelineWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public TokenPerfilIncorretoTests(AuthPipelineWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Aluno_chamando_endpoint_de_gestor_retorna_403_via_policy_declarativa()
        {
            var token = TestJwt.Valid(UserType.Aluno, Guid.NewGuid(), Guid.NewGuid());
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"/gestor/listar-usuarios/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task Administrador_criando_instrutor_retorna_403_via_checagem_imperativa()
        {
            // PodeGerenciarUsuarios (policy declarativa) deixa Administrador passar
            // — quem barra é a checagem manual dentro de ManagerController.CriarUsuario:
            // só Gestor pode criar Instrutor/Administrador (ADR-0013).
            var token = TestJwt.Valid(UserType.Administrador, Guid.NewGuid(), Guid.NewGuid());
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var body = new
            {
                Usuario = new
                {
                    IdAcademia = Guid.NewGuid(),
                    Nome = "Teste",
                    Email = "teste.instrutor@vitalitas.com",
                    Senha = "senha123",
                    DataNascimento = "1990-01-01",
                    Cpf = "12345678900",
                    Quadra = "1",
                    Rua = "Rua Teste",
                    Bairro = "Centro",
                    Cidade = "São Paulo",
                    Estado = "SP",
                    Cep = "01001-000"
                },
                Perfil = new
                {
                    TipoUsuario = "Instrutor",
                    CREF = "123456-SP"
                }
            };

            var response = await _client.PostAsJsonAsync("/gestor/criar-usuario", body);

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task Instrutor_chamando_endpoint_de_ficha_medica_de_outro_perfil_retorna_403()
        {
            // MedicalRecordController exige PodeEditarFichaMedica (só Instrutor) —
            // Aluno não deveria conseguir.
            var token = TestJwt.Valid(UserType.Aluno, Guid.NewGuid(), Guid.NewGuid());
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.PostAsJsonAsync("/ficha-medica", new { });

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }
    }
}
