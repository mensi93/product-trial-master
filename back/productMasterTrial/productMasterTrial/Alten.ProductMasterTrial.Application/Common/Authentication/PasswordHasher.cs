using System.Security.Cryptography;

namespace Alten.ProductMaster.Application.Authentication
{
    public static class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100000;

        private static readonly HashAlgorithmName Algorithme = HashAlgorithmName.SHA512;
        public static string Hash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            var pwd = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithme, HashSize);
            var res = $"{Convert.ToHexString(pwd)}-{Convert.ToHexString(salt)}";
            return res;
        }

        public static bool Verify(string password, string passwordHash)
        {
            string[] parts = passwordHash.Split('-');
            byte[] hash = Convert.FromHexString(parts[0]);
            byte[] salt = Convert.FromHexString(parts[1]);

            var inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithme, HashSize);

            return CryptographicOperations.FixedTimeEquals(hash, inputHash);
        }

    }
}
