using Infrastructure.AutoFac.FlagInterface;
using Microsoft.AspNetCore.Identity;

namespace BookShop.Core.Security.PasswordHasher
{
    public class BookShopPasswordHasher<TUser> : IScope, IPasswordHasher<TUser> where TUser : class
    {
        private readonly IHashingPasswordStrategy _hashPasswordStrategy;

        public BookShopPasswordHasher(IHashingPasswordStrategy hashPasswordStrategy)
        {
            _hashPasswordStrategy = hashPasswordStrategy;
        }

        public string HashPassword(TUser user, string password)
        {
            var hashedPassword = _hashPasswordStrategy.HashPassword(password);

            return Convert.ToBase64String(hashedPassword);
        }

        public PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword)
        {
            var hashedprovidedPassword = _hashPasswordStrategy.HashPassword(providedPassword);

            if (Convert.ToBase64String(hashedprovidedPassword).Equals(hashedPassword))
            {
                return PasswordVerificationResult.Success;
            }
            else
            {
                return PasswordVerificationResult.Failed;
            }
        }
    }

}
