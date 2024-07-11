using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml.Schema;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShortenUrl.Domain.Auth;
using ShortenUrl.Domain.Enum;
using ShortenUrl.Persistence.Repository.Contracts;
using ShortenUrl.Services.Contracts;

namespace ShortenUrl.Services.Implementations;

public class AccountService : IAccountService
{
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountService(IConfiguration configuration, IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
    {
        _configuration = configuration;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }
    
    public async Task<AuthenticationResponse> RegisterAsync(RegisterRequest registerRequest)
    {
        var identityUser = new IdentityUser
        {
            UserName = registerRequest.Email,
            Email = registerRequest.Email
        };
        
        var identityResult = await _userManager.CreateAsync(identityUser, registerRequest.Password);

        if (identityResult.Succeeded)
        {
            identityResult = await _userManager.AddToRolesAsync(identityUser, [Roles.BasicUser.ToString()]);
            
            if (identityResult.Succeeded)
            {
                string jwtSecurityToken = await GenerateJwtTokenAsync(identityUser);
            
                AuthenticationResponse response = new AuthenticationResponse()
                {
                    Id = identityUser.Id,
                    JWToken = jwtSecurityToken,
                    Email = identityUser!.Email,
                    UserName = $"{identityUser.UserName}",
                    Roles = [Roles.BasicUser.ToString()]
                };

                return response;
            }
        }

        throw new Exception("Error while registering");
    }
    
    public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
    {
        var identityUser = await _userManager.FindByEmailAsync(request.Email);

        if (identityUser != null)
        {
            var checkPasswordResult = await _userManager.CheckPasswordAsync(identityUser, request.Password);

            if (checkPasswordResult)
            {
                var roles = await _userManager.GetRolesAsync(identityUser);

                string jwtSecurityToken = await GenerateJwtTokenAsync(identityUser);
            
                AuthenticationResponse response = new AuthenticationResponse()
                {
                    Id = identityUser.Id,
                    JWToken = jwtSecurityToken,
                    Email = identityUser!.Email,
                    UserName = $"{identityUser.UserName}",
                    Roles = roles.ToList()
                };

                return response;
            }
            else
            {
                throw new InvalidDataException("Incorrect password");
            }
        }
        else
        {
            throw new InvalidDataException("User with such email does nor exist");    
        }

    }
    
    private async Task<string> GenerateJwtTokenAsync(IdentityUser identityUser)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var roles = await _userManager.GetRolesAsync(identityUser);
        
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, identityUser.Id),
            new Claim(ClaimTypes.Name, $"{identityUser.UserName}"/*user.UserName!*/),
            new Claim(ClaimTypes.Email, identityUser.Email),
            new Claim( ClaimTypes.Role, roles.FirstOrDefault())
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["TokenLifetimeMinutes"])),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}