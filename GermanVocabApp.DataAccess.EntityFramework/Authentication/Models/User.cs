using GermanVocabApp.DataAccess.EntityFramework.Core;
using GermanVocabApp.DataAccess.Shared.Abstractions;

namespace GermanVocabApp.DataAccess.EntityFramework.Authentication.Models;

public class User : EntityBase, ISoftDeletable
{
    public string Username { get; set; }
    public string Password { get; set; }
}
