using Microsoft.IdentityModel.Tokens;

namespace backend.Configurations
{
    public interface IAuthOptions
    {
        public bool ValidateIssuer { get; init; }
        public bool ValidateAudience { get; init; }
        public bool ValidateLifetime { get; init; }
        public bool ValidateIssuerSigningKey { get; init; }
        string AUDIENCE { get; init; }
        string ISSUER { get; init; }
        string KEY { get; init; }
        SymmetricSecurityKey GetSymmetricSecurityKey();
    }
}