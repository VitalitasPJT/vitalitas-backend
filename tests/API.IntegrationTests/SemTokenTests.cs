using System.Net;
using Xunit;

namespace API.IntegrationTests
{
    // HU-03: Bloquear Acessos com Credenciais Inválidas — sem token nenhum.
    public class SemTokenTests : IClassFixture<AuthPipelineWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public SemTokenTests(AuthPipelineWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Rota_com_Authorize_simples_sem_token_retorna_401()
        {
            var response = await _client.GetAsync($"/usuario/obter-tipo-usuario/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Rota_com_policy_por_role_sem_token_retorna_401()
        {
            var response = await _client.GetAsync($"/gestor/listar-usuarios/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Endpoint_novo_sem_Authorize_algum_cai_no_FallbackPolicy_e_retorna_401()
        {
            // ADR-0012: FallbackPolicy exige autenticado por padrão. Ficha médica
            // não tem [AllowAnonymous] em lugar nenhum — serve como prova de que o
            // fail-safe por omissão continua valendo.
            var response = await _client.PostAsync("/ficha-medica", content: null);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
