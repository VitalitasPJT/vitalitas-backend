using System;
using System.Collections.Generic;
using Domain.Enums;
using Application.Features.Users.Member.DTO;
using Application.Shared;

namespace Application.Features.Users.Member.Response
{
    public class MemberRP
    {
        public class ListMemberResponse
        {
            public ListMemberResponse(MemberDTO aluno, HttpStatus status)
            {
                Aluno = aluno;
                this.Status = status;
            }

            public MemberDTO Aluno { get; set; }
            public HttpStatus Status { get; set; }
        }

        public class LinkInstructorResponse
        {
            public LinkInstructorResponse(HttpStatus status)
            {
                this.Status = status;
            }

            public HttpStatus Status { get; set; }
        }

        public class UpdateGoalResponse
        {
            public UpdateGoalResponse(HttpStatus status)
            {
                this.Status = status;
            }

            public HttpStatus Status { get; set; }
        }

        public class ChangePasswordResponse
        {
            public ChangePasswordResponse(HttpStatus status)
            {
                this.Status = status;
            }

            public HttpStatus Status { get; set; }
        }


    }
}