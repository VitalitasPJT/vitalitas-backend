using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Domain.Enums;
using Domain.Features.Users.Common.Entities;
using Domain.ValueObjects;
using Xunit;

namespace API.IntegrationTests
{
    // HU-01/HU-04: login e refresh são os únicos [AllowAnonymous] do sistema —
    // precisam continuar acessíveis sem token, sem cair no FallbackPolicy
    // fail-safe (ADR-0012) nem em nenhuma AuthorizationPolicy (ADR-0013).
    public class RotasPublicasTests : IClassFixture<AuthPipelineWebApplicationFactory>
    {
        private readonly AuthPipelineWebApplicationFactory _factory;
        private readonly HttpClient _client;

        public RotasPublicasTests(AuthPipelineWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Login_sem_token_no_header_e_acessivel_e_autentica()
        {
            _factory.Users.Seed(new User(
                Guid.NewGuid(), Guid.NewGuid(), "Maria Teste", new Email("maria.publica@vitalitas.com"), "senha123",
                new DateOnly(1990, 1, 1), new CPF("12345678900"), UserType.Aluno, true, false,
                "1", "Rua Teste", "Centro", "São Paulo", "SP", "01001-000"));

            var response = await _client.PostAsJsonAsync("/usuario/login", new
            {
                Email = "maria.publica@vitalitas.com",
                Senha = "senha123"
            });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var body = await response.Content.ReadFromJsonAsync<JsonElement>();
            Assert.False(string.IsNullOrWhiteSpace(body.GetProperty("Token").GetString()));
        }

        [Fact]
        public async Task Login_com_credenciais_erradas_nao_e_barrado_pelo_pipeline_de_auth_e_sim_pela_regra_de_negocio()
        {
            // Sem Authorization header — se o FallbackPolicy estivesse pegando essa
            // rota por engano, o 401 viria do middleware antes de chegar na action.
            // O jeito de provar que chegou na action é a mensagem específica do
            // catch de credenciais inválidas do UserController.
            var response = await _client.PostAsJsonAsync("/usuario/login", new
            {
                Email = "ninguem@vitalitas.com",
                Senha = "senha-errada"
            });

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            var body = await response.Content.ReadFromJsonAsync<JsonElement>();
            Assert.Equal("Credenciais inválidas", body.GetProperty("message").GetString());
        }

        [Fact]
        public async Task Refresh_sem_token_no_header_nao_e_barrado_pelo_pipeline_de_auth_e_sim_pela_regra_de_negocio()
        {
            var response = await _client.PostAsJsonAsync("/usuario/refresh", new
            {
                AccessToken = "token-invalido",
                RefreshToken = "refresh-invalido"
            });

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            var body = await response.Content.ReadFromJsonAsync<JsonElement>();
            Assert.Equal("Token inválido ou expirado", body.GetProperty("message").GetString());
        }
    }
}
