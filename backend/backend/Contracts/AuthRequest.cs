using System.ComponentModel.DataAnnotations;

namespace backend.Contracts
{
    public record class AuthRequest
    {
        [Required(ErrorMessage = "Укажите почту для регистрации")]
        [EmailAddress]
        public required string Email { get; set; }
        [Required(ErrorMessage = "Укажите пароль для регистрации")]
        [MinLength(8)]
        public required string Password { get; set; }
    }
}
