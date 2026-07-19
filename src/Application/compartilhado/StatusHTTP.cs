
namespace Application.DTOs
{
    public class StatusHTTP
    {
        private int v1;
        private string v2;

        public string Message { get; set; }
        public int Code { get; set;}
        public bool Sucess { get; set;}

        public StatusHTTP(string v1, int code, bool v2)
        {
            this.Message = v1;
            this.Code = code;
            this.Sucess = v2;
        }

        public StatusHTTP(int v1, string v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }
    }
}