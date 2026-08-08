using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Features.Users.Common.Request
{
    public class UserRQ
    {
        public class LoginRequest
        {
            public required string Email { get; set; }
            public required string Senha { get; set; }
        }

        public class RefreshRequest
        {
            public required string AccessToken { get; set; }
            public required string RefreshToken { get; set; }
        }
    }
}
