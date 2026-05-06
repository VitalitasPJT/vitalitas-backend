
namespace Application.DTOs
{
    public class StatusHTTP
    {
        public string Message { get; set; }
        public int Code { get; set;}
        public bool Sucess { get; set;}

        public StatusHTTP(string v1, int code, bool v2)
        {
            this.Message = v1;
            this.Code = code;
            this.Sucess = v2;
        }
    }
}