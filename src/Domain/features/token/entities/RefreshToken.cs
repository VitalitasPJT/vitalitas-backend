namespace Domain.Entities
{
    public class RefreshToken
    {
        public Guid IdRefreshToken { get; private set; }
        public string TokenHash { get; private set; } = string.Empty;
        public DateTime DataExpiracao { get; private set; }
        public bool Revogado { get; private set; }
        public Guid IdUsuario { get; private set; }

        public RefreshToken(Guid idRefreshToken, string tokenHash, DateTime dataExpiracao, bool revogado, Guid usuarioId)
        {
            IdRefreshToken = idRefreshToken;
            TokenHash = tokenHash;
            DataExpiracao = dataExpiracao;
            Revogado = revogado;
            IdUsuario = usuarioId;
        }

        public RefreshToken() { }

        public void Revogar()
        {
            Revogado = true;
        }

        public bool EstaExpirado()
        {
            return DateTime.UtcNow >= DataExpiracao;
        }
    }
}
