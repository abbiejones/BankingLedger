using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

/// <summary>
/// password encryption
/// </summary>
namespace BankingLedger.Biz
{
    public static class PasswordHasher
    {
        /// <summary>
        /// creates salt and password hash from user password string
        /// </summary>
        public static Tuple<byte[], string> hashPassword(string password){
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
    
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1, //prefix appended to every password before hashing
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return new Tuple<byte[], string> (salt, hashed);
        }

        /// <summary>
        /// check whether user input password matches database stored hash
        /// </summary>
        public static bool retrievePassword(string password, byte[] salt, string origHash){
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password : password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            if (hashed.Equals(origHash)){
                return true;
            }

            return false;

        }

    }
}