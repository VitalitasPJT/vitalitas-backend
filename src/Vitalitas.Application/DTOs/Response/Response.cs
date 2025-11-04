namespace Vitalitas.Models
{
    public class LoginResponse
    {
        public string Tipo;
        public int Id;

        public Status status;
     

        public LoginResponse(string tipoUsuario, int idUsuario, Status status)
        {
            this.Tipo = tipoUsuario;
            this.Id = idUsuario;
            this.status = status;
        }
    }
}