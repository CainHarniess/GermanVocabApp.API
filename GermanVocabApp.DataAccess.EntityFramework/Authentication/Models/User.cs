using GermanVocabApp.DataAccess.EntityFramework.Core;
using GermanVocabApp.DataAccess.Shared.Abstractions;

namespace GermanVocabApp.DataAccess.EntityFramework.Authentication.Models;

public class User : EntityBase, ISoftDeletable
{
    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public string Username { get; set; }
    public string Password { get; set; }
}
