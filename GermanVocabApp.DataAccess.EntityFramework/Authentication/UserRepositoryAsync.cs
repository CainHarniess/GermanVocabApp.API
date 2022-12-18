using GermanVocabApp.DataAccess.EntityFramework.Authentication.Conversion;
using GermanVocabApp.DataAccess.EntityFramework.Authentication.Models;
using GermanVocabApp.DataAccess.EntityFramework.Core;
using GermanVocabApp.DataAccess.Shared.Users.Models;
using GermanVocabApp.DataAccess.Shared.Vocab;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GermanVocabApp.DataAccess.EntityFramework.Vocab;

public class UserRepositoryAsync : RepositoryBase, IUserRepositoryAsync
{
    public UserRepositoryAsync(GermanAppAppDbContext context) : base(context)
    {

    }

    public async Task<UserDto?> Get(string username, string password)
    {
        // TODO: Add no tracking.
        User? user = await Context.Users
                                  .FirstOrDefaultAsync(u => u.Username == username
                                                         && u.Password == password);
        return user?.ToDto();
    }

    public async Task<UserDto> Create(UserDto dto)
    {
        Debug.Assert(!dto.Id.HasValue, $"{nameof(UserDto)} should not have a non-default ID value when used to create a new entity.");

        bool existingUser = await Context.Users
                                         .AnyAsync(u => u.Username == dto.Username);
        if (existingUser)
        {
            return null;
        }

        User entity = new(dto.Username, dto.Password);
        _ = await Context.AddAsync(entity);
        return entity.ToDto();
    }
}
