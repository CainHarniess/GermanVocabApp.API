using GermanVocabApp.Api.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
            string secret = builder.Configuration[AuthenticationConstants.SecretKey];
            byte[] secretBytes = Encoding.UTF8.GetBytes(secret);

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

    public static WebApplicationBuilder AddSwaggerConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(o =>
        {
            o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            o.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        return builder;
    }
}
