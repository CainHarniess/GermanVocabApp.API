using GermanVocabApp.Api.Authentication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GermanVocabApp.Api.Authentication;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public IActionResult Authenticate(AuthenticationRequest request)
    {
        if (request.UserName != "CainHarniess" || request.Password != "Password")
        {
            return BadRequest();
        }

        SigningCredentials credentials = CreateSigningCredentials();
        string tokenString = CreateTokenString(credentials);
        return Ok(new AuthenticationResponse(tokenString));
    }

    private static SigningCredentials CreateSigningCredentials()
    {
        byte[] secretBytes = Encoding.UTF8.GetBytes(AuthenticationConstants.Secret);
        SymmetricSecurityKey key = new(secretBytes);
        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);
        return credentials;
    }

    private static string CreateTokenString(SigningCredentials credentials)
    {
        JwtSecurityToken token = CreateToken(credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
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
