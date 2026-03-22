using Vitalitas.Backend.Domain.Entities;

namespace Vitalitas.Application.DTOs.Request
{
    public class LoginRequest
    {
        public string Email { get; set;}
        public string Password { get; set;}
    }

    public class PasswordResetRequest
    {
        public int Id { get; set;}
        public string NewPassword { get; set;}
    }
}