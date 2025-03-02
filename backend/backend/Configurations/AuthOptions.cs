using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace backend.Configurations
{
    /// <summary>
    /// Singleton => getInstance()
    /// </summary>
    public class AuthOptions : IAuthOptions
    {
        private static AuthOptions? instance;

        public bool ValidateIssuer { get; init; }
        public bool ValidateAudience { get; init; }
        public bool ValidateLifetime { get; init; }
        public bool ValidateIssuerSigningKey { get; init; }
        public string ISSUER { get; init; }
        public string AUDIENCE { get; init; }
        public string KEY { get; init; }
        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }

        private AuthOptions()
        {
            string? env;

            env = Environment.GetEnvironmentVariable("AUTH_OPTIONS_ISSUER_VALIDATE");
            ValidateIssuer = String.IsNullOrEmpty(env) ? true : Convert.ToBoolean(env);
            env = Environment.GetEnvironmentVariable("AUTH_OPTIONS_AUDIENCE_VALIDATE");
            ValidateAudience = String.IsNullOrEmpty(env) ? true : Convert.ToBoolean(env);
            env = Environment.GetEnvironmentVariable("AUTH_OPTIONS_LIFE_TIME_VALIDATE");
            ValidateLifetime = String.IsNullOrEmpty(env) ? true : Convert.ToBoolean(env);
            env = Environment.GetEnvironmentVariable("AUTH_OPTIONS_SECRET_KEY_VALIDATE");
            ValidateIssuerSigningKey = String.IsNullOrEmpty(env) ? true : Convert.ToBoolean(env);



            ISSUER = CustomServiceEnv.GetEnv("AUTH_OPTIONS_ISSUER");
            AUDIENCE = CustomServiceEnv.GetEnv("AUTH_OPTIONS_AUDIENCE");
            KEY = CustomServiceEnv.GetEnv("AUTH_OPTIONS_SECRET_KEY");
        }

        public static AuthOptions getInstance()
        {
            if (instance == null)
            {
                instance = new AuthOptions();
            }
            return instance;
        }

    }
}
