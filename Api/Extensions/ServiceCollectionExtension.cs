using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using Api.Services;

namespace Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddToken(this IServiceCollection services)
        {
            var signingConfigurations = new SigningConfiguration();
            services.AddSingleton(signingConfigurations);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(bearerOptions =>
           {
               var paramsValidation = bearerOptions.TokenValidationParameters;
               paramsValidation.IssuerSigningKey = signingConfigurations.Key;
               paramsValidation.ValidateIssuerSigningKey = true;
               paramsValidation.ValidateLifetime = true;
               paramsValidation.ValidAudience = "SomeIssuer";
               paramsValidation.ValidIssuer = "SomeAudience";
               paramsValidation.ClockSkew = TimeSpan.Zero;
           });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            return services;
        }
    }
}