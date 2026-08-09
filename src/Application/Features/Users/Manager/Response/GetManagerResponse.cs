using Application.Shared;
using Application.Features.Users.Manager.DTO;

namespace Application.Features.Users.Manager.Response
{
    public class GetManagerResponse
    {
        public GetManagerResponse(ManagerDTO? gestor, HttpStatus status)
        {
            Gestor = gestor;
            Status = status;
        }

        public ManagerDTO? Gestor { get; set; }
        public HttpStatus Status { get; set; }
    }
}
