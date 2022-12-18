using GermanVocabApp.DataAccess.Shared.Core;

namespace GermanVocabApp.DataAccess.Shared.Users.Models;

public class UserDto : EntityDtoBase
{
    public UserDto(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public string Username { get; set; }
    public string Password { get; set; }
}
