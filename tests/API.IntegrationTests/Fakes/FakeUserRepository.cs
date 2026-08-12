using System.Collections.Concurrent;
using Domain.Enums;
using Domain.Features.Shared.Entities;
using Domain.Features.Users.Common.Entities;
using Domain.Features.Users.Common.Interfaces;

namespace API.IntegrationTests.Fakes
{
    // Substitui o UserRepository (Dapper contra Azure SQL) nos testes de
    // integração do pipeline de auth — as 4 HUs cobertas aqui são sobre
    // UseAuthentication/UseAuthorization/[Authorize]/policies, não persistência.
    public class FakeUserRepository : IUserRepository
    {
        private readonly ConcurrentDictionary<string, User> _porEmail = new(StringComparer.OrdinalIgnoreCase);
        private readonly ConcurrentDictionary<Guid, User> _porId = new();

        public void Seed(User usuario)
        {
            _porEmail[usuario.Email.Valor] = usuario;
            _porId[usuario.IdUsuario] = usuario;
        }

        public User Login(string email, string senha)
        {
            if (_porEmail.TryGetValue(email, out var usuario) && usuario.Senha == senha)
                return usuario;
            return null!;
        }

        public ActivityLog RegistrarAcao(Guid idUsuario, int acao, string dispositivoLogado, string localizacao)
            => throw new NotImplementedException("Não usado nos testes de pipeline de auth.");

        public UserType? GetTipoUsuario(Guid idUsuario)
            => _porId.TryGetValue(idUsuario, out var usuario) ? usuario.TipoUsuario : null;

        public bool TrocarSenha(Guid idusuario, string novasenha)
            => throw new NotImplementedException("Não usado nos testes de pipeline de auth.");

        public Guid GetIdAcademia(Guid idUsuario)
            => _porId.TryGetValue(idUsuario, out var usuario)
                ? usuario.IdAcademia
                : throw new Exception($"Academia não encontrada para o usuário {idUsuario}");
    }
}
