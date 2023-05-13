using Infrastructure.AutoFac.FlagInterface;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace BookShop.Core.Security.PasswordHasher
{
    public class HashingPassowordStrategy : IHashingPasswordStrategy, IScope
    {
        public byte[] HashPassword(
            string password,
            KeyDerivationPrf pbkdf2Algo,
            int pbkdf2IterationCount,
            int pbkdf2SubkeyLength,
            int saltSize
            )
        {
            var salt = RandomNumberGenerator.GetBytes(saltSize);

            var subKey = KeyDerivation.Pbkdf2(password, salt, pbkdf2Algo, pbkdf2IterationCount, pbkdf2SubkeyLength);

            var outputBytes = new byte[saltSize + pbkdf2SubkeyLength];
            Buffer.BlockCopy(salt, 0, outputBytes, 0, saltSize);
            Buffer.BlockCopy(subKey, 0, outputBytes, saltSize, pbkdf2SubkeyLength);

            return outputBytes;
        }

        public byte[] HashPassword(string password)
        {
            const KeyDerivationPrf PBKDF2_ALGO = KeyDerivationPrf.HMACSHA1;
            const int PBKDF2_ITERATION_COUNT = 1000;
            const int PBKDF2_SUBKEY_LENGTH = 5;
            const int SALT_SIZE = 4;

            return HashPassword(
                password,
                PBKDF2_ALGO,
                PBKDF2_ITERATION_COUNT,
                PBKDF2_SUBKEY_LENGTH,
                SALT_SIZE);
        }
    }

}
