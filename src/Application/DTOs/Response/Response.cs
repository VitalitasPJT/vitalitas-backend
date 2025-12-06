namespace Vitalitas.Backend.Application.DTOs
{
    public class LoginResponse
    {
        public string Tipo { get; set;}
        public int Id { get; set;}
        public bool SenhaFlag {get; set;}

        public Status status { get; set;}
     

        public LoginResponse(string tipoUsuario, int idUsuario, bool senhaFlag, Status status)
        {
            this.Tipo = tipoUsuario;
            this.Id = idUsuario;
            this.SenhaFlag = senhaFlag;
            this.status = status;
        }
    }
}