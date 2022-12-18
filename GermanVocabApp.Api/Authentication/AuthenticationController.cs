using GermanVocabApp.Api.Authentication.Conversion;
using GermanVocabApp.Api.Authentication.Models;
using GermanVocabApp.DataAccess.Shared.Users.Models;
using GermanVocabApp.DataAccess.Shared.Vocab;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GermanVocabApp.Api.Authentication;

[ApiController]
[Route("api")]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepositoryAsync _repository;

    public AuthenticationController(IConfiguration configuration, IUserRepositoryAsync repository)
    {
        _configuration = configuration;
        _repository = repository;
    }

    [HttpPost("authenticate")]
    [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public IActionResult Authenticate(AuthenticationRequest request)
    {
        if (request.UserName != "CainHarniess" || request.Password != "Password")
        {
            return BadRequest();
        }

        string tokenString = CreateTokenString();
        return Ok(new AuthenticationResponse(tokenString));
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(RegistrationRequest request)
    {
        UserDto newUserDto = request.ToDto();
        UserDto? createdUserDto = await _repository.Create(newUserDto);

        if (createdUserDto == null)
        {
            return BadRequest($"Username {request.Username} is already in use.");
        }

        string tokenString = CreateTokenString();
        return Ok(new AuthenticationResponse(tokenString));
    }

    private string CreateTokenString()
    {
        SigningCredentials credentials = CreateSigningCredentials();
        JwtSecurityToken token = CreateToken(credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private SigningCredentials CreateSigningCredentials()
    {
        string secret = _configuration[AuthenticationConstants.SecretKey];
        byte[] secretBytes = Encoding.UTF8.GetBytes(secret);
        SymmetricSecurityKey key = new(secretBytes);
        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);
        return credentials;
    }

    private static JwtSecurityToken CreateToken(SigningCredentials credentials)
    {
        return new(AuthenticationConstants.Issuer,
                   AuthenticationConstants.Audience,
                   new List<Claim>(),
                   null,
                   DateTime.Now.AddHours(1),
                   credentials);
    }
}
