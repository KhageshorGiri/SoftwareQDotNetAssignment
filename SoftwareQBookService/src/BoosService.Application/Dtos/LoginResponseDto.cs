using System.ComponentModel.DataAnnotations;

namespace BookService.Application.Dtos;

public record LoginResponseDto
{
    public UserDto User { get; set; }
    public string Token { get; set; }
}

public class LoginRequestDto
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
}

public class RegistrationRequestDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(300)]
    public string Name { get; set; }

    [Required]
    [StringLength(10)]
    public string PhoneNumber { get; set; }

    [Required]
    public string Password { get; set; }

    public string? Role { get; set; }
}