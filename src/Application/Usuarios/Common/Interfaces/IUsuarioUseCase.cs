using static Application.Usuarios.Common.Response.UsuarioRS;

namespace Application.Usuarios.Common.Interfaces
{
    public interface IUsuarioUseCase
    {
        LoginResponse Login(string email, string senha);
        AdicionarLogResponse AdicionarLog(Guid idusuario, int acao, string dispositivoLogado, string localizacao);
        RefreshResponse RefreshToken(string accessToken, string refreshToken);
        ObterTipoUsuarioResponse ObterTipoUsuario(Guid idUsuario);
    }
}