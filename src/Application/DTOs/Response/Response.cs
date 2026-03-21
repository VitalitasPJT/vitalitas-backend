namespace Vitalitas.Backend.Application.DTOs
{
    public class LoginResponse
    {
        public string Tipo { get; set;}
        public int Id { get; set;}

        public bool SenhaFlag {get; set;}

        public Status Status { get; set;}
     
        public LoginResponse(string tipoUsuario, int idUsuario, bool senhaFlag, Status statuspass)
        {
            this.Tipo = tipoUsuario;
            this.Id = idUsuario;
            this.SenhaFlag = senhaFlag;
            this.Status = statuspass;
        }
    }

    public class PasswordResetResponse
    {
     
        public Status status { get; set;}
     
        public PasswordResetResponse(Status status)
        {
            this.status = status;
        }
    }
    
}