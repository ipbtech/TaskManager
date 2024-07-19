using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TaskManager.API.Extensions
{
    public static class AuthenticationExtension
    {
        public static void AddJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true, // указывает, будет ли валидироваться издатель при валидации токена
                        ValidIssuer = AuthOptions.ISSUER, // строка, представляющая издателя
                        ValidateAudience = true, // будет ли валидироваться потребитель токена
                        ValidAudience = AuthOptions.AUDIENCE, // установка потребителя токена
                        ValidateLifetime = true, // будет ли валидироваться время существования
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(), // установка ключа безопасности
                        ValidateIssuerSigningKey = true, // валидация ключа безопасности
                    };
                });
        }
    }

    public class AuthOptions
    {
        public const string ISSUER = "TaskManagerWebApi"; // издатель токена
        public const string AUDIENCE = "TaskManagerClient"; // потребитель токена
        const string KEY = "R2pVqyTkN89Ju7b6sEcAX35ghKfZmoWd";   // ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена
        public static SymmetricSecurityKey GetSymmetricSecurityKey() => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }
}
