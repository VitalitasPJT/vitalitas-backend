using Microsoft.Identity.Client;
using Vitalitas.Backend.Domain.Entities;

namespace Vitalitas.Backend.Application.DTOs
{
    public class LoginRequest
    {
        public string Email { get; set;}
        public string Password { get; set;}
    }
}