namespace GermanVocabApp.Api.Authentication.Models;

public class AuthenticationRequest
{
    public AuthenticationRequest(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }

    public string UserName { get; }
    public string Password { get; }
}
