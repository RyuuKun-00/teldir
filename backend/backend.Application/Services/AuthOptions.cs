using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace backend.Application.Services
{
    public class AuthOptions
    {
        public string Issuer { get; set; } = String.Empty; //издатель токена
        public string Audience { get; set; } = String.Empty;  //потребитель токена
        public string SecretKey { get; set; } = String.Empty; //секретный ключ для подписи
        public bool ValidateAudience { get; set; } //проверять потребителя
        public bool ValidateIssuer { get; set; } //проверять издателя
        public bool ValidateLifetime { get; set; } //проверять время жизни токена
        public bool ValidateIssuerSigningKey { get; set; } //проверять подпись
        public int AccessTokenLifetimeMinute { get; set; } //время жизни токена в минутах
        public int RefreshTokenLifetimeMinute { get; set; } //время жизни токена в минутах

        public SymmetricSecurityKey GetSymmetricSecurityKey
        {
            get
            {
                return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            }
        }

        public TokenValidationParameters GetTokenValidationParameters(bool validateLifetime)
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = ValidateIssuer,
                ValidIssuer = Issuer,
                ValidateAudience = ValidateAudience,
                ValidAudience = Audience,
                ValidateLifetime = validateLifetime,
                IssuerSigningKey = GetSymmetricSecurityKey,
                ValidateIssuerSigningKey = ValidateIssuerSigningKey
            };
        }
    }
}
