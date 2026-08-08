using static Application.Features.Users.Common.Response.UserRS;

namespace Application.Features.Users.Common.Interfaces
{
    public interface IUserUseCase
    {
        LoginResponse Login(string email, string senha);
        AddLogResponse AdicionarLog(Guid idusuario, int acao, string dispositivoLogado, string localizacao);
        RefreshResponse RefreshToken(string accessToken, string refreshToken);
        GetUserTypeResponse ObterTipoUsuario(Guid idUsuario);
    }
}