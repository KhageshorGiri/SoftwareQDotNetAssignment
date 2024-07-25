using BookService.Application.Dtos;

namespace BookService.Application.IBusinesses;

public interface IAuthService
{
    Task<string> Register(RegistrationRequestDto registrationRequestDto);
    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    Task<bool> AssignRole(string email, string roleName);
}
