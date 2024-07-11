using Microsoft.AspNetCore.Mvc;
using ShortenUrl.Domain.Auth;
using ShortenUrl.Services.Contracts;

namespace ShortenUrl1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        var response = await _accountService.RegisterAsync(registerRequest);
        
        return Ok(response);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthenticationRequest authenticationRequest)
    {
        var response = await _accountService.AuthenticateAsync(authenticationRequest);
        
        return Ok(response);
    }
}