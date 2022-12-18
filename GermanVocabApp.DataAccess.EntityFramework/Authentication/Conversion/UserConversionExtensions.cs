using GermanVocabApp.DataAccess.EntityFramework.Authentication.Models;
using GermanVocabApp.DataAccess.Shared.Users.Models;
using System.Diagnostics;

namespace GermanVocabApp.DataAccess.EntityFramework.Authentication.Conversion;
public static class UserConversionExtensions
{
    public static UserDto ToDto(this User entity)
    {
        Debug.Assert(entity.Id != default, "Entities should have a non-default ID when being converted to a DTO.");
        return new UserDto(entity.Username, entity.Password)
        {
            Id = entity.Id,
        };
    }
}
