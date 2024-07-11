using System.ComponentModel.DataAnnotations;

namespace ShortenUrl.Domain.Auth;

public class RegisterRequest
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}