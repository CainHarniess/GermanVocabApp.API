using GermanVocabApp.DataAccess.EntityFramework.Authentication.Conversion;
using GermanVocabApp.DataAccess.EntityFramework.Authentication.Models;
using GermanVocabApp.DataAccess.EntityFramework.Core;
using GermanVocabApp.DataAccess.Shared.Users.Models;
using GermanVocabApp.DataAccess.Shared.Vocab;
using Microsoft.EntityFrameworkCore;

namespace GermanVocabApp.DataAccess.EntityFramework.Vocab;

public class UserRepositoryAsync : RepositoryBase, IUserRepositoryAsync
{
    public UserRepositoryAsync(GermanAppAppDbContext context) : base(context)
    {

    }

    public async Task<UserDto?> Get(string username, string password)
    {
        User? user = await Context.Users
                                  .FirstOrDefaultAsync(u => u.Username == username
                                                         && u.Password == password);
        return user?.ToDto();
    }
}
