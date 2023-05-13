using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace BookShop.Core.Security.PasswordHasher
{
    public interface IHashingPasswordStrategy
    {
        public byte[] HashPassword(string password, KeyDerivationPrf pbkdf2Algo, int pbkdf2IterationCount, int pbkdf2SubkeyLength, int saltSize);

        public byte[] HashPassword(string password);
    }

}
