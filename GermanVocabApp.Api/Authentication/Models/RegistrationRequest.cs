namespace GermanVocabApp.Api.Authentication.Models;

public class RegistrationRequest
{
    public RegistrationRequest(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public string Username { get; set; }
    public string Password { get; set; }
}