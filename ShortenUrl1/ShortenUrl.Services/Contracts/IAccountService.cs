using ShortenUrl.Domain.Auth;

namespace ShortenUrl.Services.Contracts;

public interface IAccountService
{
    Task<AuthenticationResponse> RegisterAsync(RegisterRequest registerRequest);
    Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
}
