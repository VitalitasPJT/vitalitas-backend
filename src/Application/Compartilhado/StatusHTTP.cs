
namespace Application.Compartilhado
{
    public class StatusHTTP
    {
        public string Message { get; set; }
        public int Code { get; set;}
        public bool Sucess { get; set;}

        public StatusHTTP(string message, int code, bool sucess)
        {
            this.Message = message;
            this.Code = code;
            this.Sucess = sucess;
        }
    }
}