using GermanVocabApp.Api.Authentication.Models;
using GermanVocabApp.DataAccess.Shared.Users.Models;

namespace GermanVocabApp.Api.Authentication.Conversion;

public static class RegistrationRequestConversionExtensions
{
    public static UserDto ToDto(this RegistrationRequest request)
    {
        return new UserDto(request.Username, request.Password);
    }
}
