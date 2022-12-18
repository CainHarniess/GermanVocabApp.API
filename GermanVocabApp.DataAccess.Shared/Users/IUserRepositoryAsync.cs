using GermanVocabApp.DataAccess.Shared.Users.Models;

namespace GermanVocabApp.DataAccess.Shared.Vocab;

public interface IUserRepositoryAsync
{
    Task<UserDto> Create(UserDto dto);
    Task<UserDto?> Get(string username, string password);
}
