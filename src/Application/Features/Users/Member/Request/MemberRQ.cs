using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Member.Request
{
    public class MemberRQ
    {
         
        public class ListMemberRequest
        {
            public required Guid IdAluno { get; set; }
        }

        public class LinkInstructorRequest
        {
            public Guid IdAluno { get; set; }
            public Guid IdInstrutor { get; set; }
        }

        public class UpdateGoalRequest
        {
            public Guid IdAluno { get; set; }
            public string NovoObjetivo { get; set; }
        }

        public class ChangePasswordRequest
        {
            public required Guid IdUsuario { get; set; }
            public required string NovaSenha { get; set; }
        }
    }
}
