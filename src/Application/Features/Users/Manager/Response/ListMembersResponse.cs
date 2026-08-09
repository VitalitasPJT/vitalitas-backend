using System.Collections.Generic;
using Application.Shared;
using Application.Features.Users.Member.DTO;

namespace Application.Features.Users.Manager.Response
{
    public class ListMembersResponse
    {
        public ListMembersResponse(List<MemberDTO> alunos, HttpStatus status)
        {
            Alunos = alunos;
            Status = status;
        }

        public List<MemberDTO> Alunos { get; set; }
        public HttpStatus Status { get; set; }
    }
}
