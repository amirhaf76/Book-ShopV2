using Infrastructure.AutoFac.FlagInterface;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace BookShop.Core.Security.PasswordHasher
{
    public class BookShopPasswordHasher<TUser> : IScope, IPasswordHasher<TUser> where TUser : class
    {
        private const KeyDerivationPrf PBKDF2_ALGO = KeyDerivationPrf.HMACSHA1;
        private const int PBKDF2_ITERATION_COUNT = 1000;
        private const int PBKDF2_SUBKEY_LENGTH = 5;
        private const int SALT_SIZE = 4;

        public BookShopPasswordHasher()
        {

        }

        public string HashPassword(TUser user, string password)
        {
            var hashedPassword = HashPassword(password);

            return Convert.ToBase64String(hashedPassword);
        }

        public PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword)
        {
            return VerifyHashedPassword(hashedPassword, providedPassword);
        }

        private static byte[] HashPassword(
            string password,
            int saltSize,
            KeyDerivationPrf pbkdf2Algo,
            int pbkdf2IterationCount,
            int pbkdf2SubkeyLength)
        {
            var salt = RandomNumberGenerator.GetBytes(saltSize);

            return HashPassword(password, salt, pbkdf2Algo, pbkdf2IterationCount, pbkdf2SubkeyLength);
        }

        private static byte[] HashPassword(
            string password,
            byte[] salt,
            KeyDerivationPrf pbkdf2Algo,
            int pbkdf2IterationCount,
            int pbkdf2SubkeyLength)
        {
            var saltSize = salt.Length;

            var subKey = KeyDerivation.Pbkdf2(password, salt, pbkdf2Algo, pbkdf2IterationCount, pbkdf2SubkeyLength);

            var outputBytes = new byte[saltSize + pbkdf2SubkeyLength];

            Buffer.BlockCopy(salt, 0, outputBytes, 0, saltSize);
            Buffer.BlockCopy(subKey, 0, outputBytes, saltSize, pbkdf2SubkeyLength);

            return outputBytes;
        }

        private static byte[] HashPassword(string password)
        {
            return HashPassword(
                password,
                SALT_SIZE,
                PBKDF2_ALGO,
                PBKDF2_ITERATION_COUNT,
                PBKDF2_SUBKEY_LENGTH);
        }

        private static byte[] HashPassword(string password, byte[] salt)
        {
            return HashPassword(
                password,
                salt,
                PBKDF2_ALGO,
                PBKDF2_ITERATION_COUNT,
                PBKDF2_SUBKEY_LENGTH);
        }

        private static PasswordVerificationResult VerifyHashedPassword(
            string hashedPasswordBlocks,
            string providedPassword)
        {
            var passwordData = Convert.FromBase64String(hashedPasswordBlocks);

            var salt = passwordData.AsSpan(0, SALT_SIZE).ToArray();

            var providedHashedPassword = HashPassword(providedPassword, salt);

            if (Convert.ToBase64String(providedHashedPassword).Equals(hashedPasswordBlocks))
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
