using System.ComponentModel.DataAnnotations;

namespace API.Settings
{
    public class JwtSettings
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "'Jwt:Key' está ausente ou vazio.")]
        public string Key { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "'Jwt:Issuer' está ausente ou vazio.")]
        public string Issuer { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "'Jwt:Audience' está ausente ou vazio.")]
        public string Audience { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "'Jwt:DurationInMinutes' deve ser um inteiro positivo.")]
        public int DurationInMinutes { get; set; }
    }
}
