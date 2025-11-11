using Microsoft.AspNetCore.Mvc;

namespace Vitalitas.Backend.Application.DTOs
{
    public class Status
    {
        public string Message;
        public int Code;
        public bool Sucess;

        public Status(string v1, int code, bool v2)
        {
            this.Message = v1;
            this.Code = code;
            this.Sucess = v2;
        }
    }
}