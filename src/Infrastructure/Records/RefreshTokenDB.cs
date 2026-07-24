namespace Infrastructure.Records
{
    public class RefreshTokenDB
    {
        public Guid IdRefreshToken { get; set; }
        public string TokenHash { get; set; } = string.Empty;
        public DateTime DataExpiracao { get; set; }
        public bool Revogado { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
