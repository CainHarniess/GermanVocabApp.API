using GermanVocabApp.Api.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GermanVocabApp.Api.DependencyInjection;

public static class WebApplicationBuilderServiceExtensions
{
    public static WebApplicationBuilder AddJwtAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
        {
            byte[] secretBytes = Encoding.UTF8.GetBytes(AuthenticationConstants.Secret);
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = AuthenticationConstants.Issuer,
                ValidAudience = AuthenticationConstants.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
            };
        });
        return builder;
    }
}
