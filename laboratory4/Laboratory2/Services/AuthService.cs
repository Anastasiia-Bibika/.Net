using Laboratory2.Data;
using Laboratory2.Models;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Options;
using Laboratory2.Helpers;
namespace Laboratory2.Services
{
    public interface IAuthService
    {
        TokenedUser Authenticate(string identityType, string identity, string secret);
    }

    public class AuthService : IAuthService
    {
        private readonly AppSettings settings;
        private readonly AccountContext context;

        public AuthService(IOptions<AppSettings> settings, AccountContext context)
        {
            this.settings = settings.Value;
            this.context = context;
        }

        public TokenedUser Authenticate(string identityType, string identity, string secret)
        {
            User user = null;

            if (identityType == "username")
            {
                user = context.Users.FirstOrDefault(u => u.Name == identity && u.Password == CryptoHelper.EncryptPassword(secret));
            }
            else if (identityType == "email")
            {
                user = context.Users.FirstOrDefault(u => u.Email == identity && u.Password == CryptoHelper.EncryptPassword(secret));
            }

            if (user == null) return null;

            var token = AuthHelper.GenerateJwtToken(settings.JwtKey, settings.JwtIssuer, user);

            return new TokenedUser(user, token);
        }
    }
}
