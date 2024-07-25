using BookService.Application.Dtos;
using BookService.Application.IBusinesses;
using BookService.Shared.OperaionResponse;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BookService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;


    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<OperationResponse> Register([FromBody] RegistrationRequestDto model)
    {
        if (!ModelState.IsValid)
            return OperationResponse.Failure(ModelState.ToString());

        var errorMessage = await _authService.Register(model);
        if (!string.IsNullOrEmpty(errorMessage))
            return OperationResponse.Failure(errorMessage);

        return OperationResponse.Success("User Registerd Succefully.");
    }

    [HttpPost("login")]
    public async Task<OperationResponse<LoginResponseDto>> Login([FromBody] LoginRequestDto model)
    {
        if (!ModelState.IsValid)
            return OperationResponse<LoginResponseDto>.Failure(ModelState.ToString());

        var loginResponse = await _authService.Login(model);
        if (loginResponse.User == null)
            return OperationResponse<LoginResponseDto>.Failure("Invalid Username or Password.");

        _logger.LogInformation("Request For Login for {0} Completed with {1}", JsonSerializer.Serialize(model), loginResponse);
        return OperationResponse<LoginResponseDto>.Success("Success", loginResponse);
    }
}
