using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Domain.Enums;
using Domain.Features.Token.Entities;
using Domain.Features.Users.Common.Entities;
using Domain.ValueObjects;
using Xunit;

namespace API.IntegrationTests
{
    // HU-02: Validar Token JWT nas Requisições / HU-04: Automatizar Validação de
    // Segurança — token expirado é rejeitado, e o ciclo de renovação via
    // /usuario/refresh funciona (ou é rejeitado) conforme o refresh token.
    public class TokenExpiradoTests : IClassFixture<AuthPipelineWebApplicationFactory>
    {
        private readonly AuthPipelineWebApplicationFactory _factory;
        private readonly HttpClient _client;

        public TokenExpiradoTests(AuthPipelineWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Token_expirado_em_rota_protegida_retorna_401()
        {
            var token = TestJwt.Expired(UserType.Aluno, Guid.NewGuid(), Guid.NewGuid());
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"/usuario/obter-tipo-usuario/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Refresh_com_token_expirado_e_refresh_token_valido_devolve_200_com_token_novo()
        {
            var idUsuario = Guid.NewGuid();
            var accessTokenExpirado = TestJwt.Expired(UserType.Aluno, idUsuario, Guid.NewGuid());

            const string rawRefreshToken = "refresh-valido-de-teste";
            _factory.RefreshTokens.Save(new RefreshToken(
                Guid.NewGuid(), ComputeHash(rawRefreshToken), DateTime.UtcNow.AddDays(1), false, idUsuario));

            var response = await _client.PostAsJsonAsync("/usuario/refresh", new
            {
                AccessToken = accessTokenExpirado,
                RefreshToken = rawRefreshToken
            });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var body = await response.Content.ReadFromJsonAsync<JsonElement>();
            Assert.False(string.IsNullOrWhiteSpace(body.GetProperty("AccessToken").GetString()));
            Assert.False(string.IsNullOrWhiteSpace(body.GetProperty("RefreshToken").GetString()));
        }

        [Fact]
        public async Task Novo_access_token_emitido_pelo_refresh_funciona_numa_rota_protegida()
        {
            var idUsuario = Guid.NewGuid();
            var accessTokenExpirado = TestJwt.Expired(UserType.Aluno, idUsuario, Guid.NewGuid());

            _factory.Users.Seed(new User(
                idUsuario, Guid.NewGuid(), "Usuário Teste", new Email("refresh.chain@vitalitas.com"), "senha123",
                new DateOnly(1990, 1, 1), new CPF("12345678900"), UserType.Aluno, true, false,
                "1", "Rua Teste", "Centro", "São Paulo", "SP", "01001-000"));

            const string rawRefreshToken = "refresh-valido-encadeado";
            _factory.RefreshTokens.Save(new RefreshToken(
                Guid.NewGuid(), ComputeHash(rawRefreshToken), DateTime.UtcNow.AddDays(1), false, idUsuario));

            var refreshResponse = await _client.PostAsJsonAsync("/usuario/refresh", new
            {
                AccessToken = accessTokenExpirado,
                RefreshToken = rawRefreshToken
            });
            var novoToken = (await refreshResponse.Content.ReadFromJsonAsync<JsonElement>())
                .GetProperty("AccessToken").GetString();

            using var request = new HttpRequestMessage(HttpMethod.Get, $"/usuario/obter-tipo-usuario/{idUsuario}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", novoToken);
            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Refresh_com_refresh_token_revogado_retorna_401()
        {
            var idUsuario = Guid.NewGuid();
            var accessTokenExpirado = TestJwt.Expired(UserType.Aluno, idUsuario, Guid.NewGuid());

            const string rawRefreshToken = "refresh-revogado-de-teste";
            var stored = new RefreshToken(Guid.NewGuid(), ComputeHash(rawRefreshToken), DateTime.UtcNow.AddDays(1), false, idUsuario);
            _factory.RefreshTokens.Save(stored);
            _factory.RefreshTokens.Revoke(stored.IdRefreshToken);

            var response = await _client.PostAsJsonAsync("/usuario/refresh", new
            {
                AccessToken = accessTokenExpirado,
                RefreshToken = rawRefreshToken
            });

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Refresh_com_refresh_token_expirado_retorna_401()
        {
            var idUsuario = Guid.NewGuid();
            var accessTokenExpirado = TestJwt.Expired(UserType.Aluno, idUsuario, Guid.NewGuid());

            const string rawRefreshToken = "refresh-com_prazo_vencido";
            _factory.RefreshTokens.Save(new RefreshToken(
                Guid.NewGuid(), ComputeHash(rawRefreshToken), DateTime.UtcNow.AddDays(-1), false, idUsuario));

            var response = await _client.PostAsJsonAsync("/usuario/refresh", new
            {
                AccessToken = accessTokenExpirado,
                RefreshToken = rawRefreshToken
            });

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        private static string ComputeHash(string rawToken)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawToken));
            return Convert.ToHexString(bytes).ToLowerInvariant();
        }
    }
}
