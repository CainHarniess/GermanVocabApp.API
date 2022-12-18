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
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = "https://localhost:7084",
                ValidAudience = "https://localhost:7084",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("4a2f5bc1-cde4-4fc9-82d5-a076f4d07071")),
            };
        });
        return builder;
    }
}
