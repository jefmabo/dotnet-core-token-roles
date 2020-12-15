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
            var signingConfiguration = new SigningConfiguration();
            services.AddSingleton(signingConfiguration);
            
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(bearerOptions =>
           {
               var paramsValidation = bearerOptions.TokenValidationParameters;
               paramsValidation.IssuerSigningKey = signingConfiguration.Key;
               paramsValidation.ValidAudience = "someAudience";
               paramsValidation.ValidIssuer = "someIssuer";
               paramsValidation.ValidateIssuerSigningKey = true;
               paramsValidation.ValidateLifetime = true;
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