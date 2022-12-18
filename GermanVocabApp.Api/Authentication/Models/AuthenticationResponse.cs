namespace GermanVocabApp.Api.Authentication.Models;

public class AuthenticationResponse
{
    public AuthenticationResponse(string token)
    {
        Token = token;
    }
    public string Token { get; }
}