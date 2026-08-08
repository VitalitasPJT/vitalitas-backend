using Application.Shared;

namespace Application.Features.Users.Manager.Response
{
    public class ListUserResponse
    {
        public ListUserResponse(dynamic usuario, HttpStatus status)
        {
            Usuario = usuario;
            Status = status;
        }

        public dynamic Usuario { get; set; }
        public HttpStatus Status { get; set; }
    }
}
