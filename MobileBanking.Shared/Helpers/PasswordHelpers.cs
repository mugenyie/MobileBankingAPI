using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MobileBanking.Shared.Helpers
{
    public static class PasswordHelpers
    {
        public static string ComputeHash(byte[] bytesToHash, byte[] salt)
        {
            var byteResult = new Rfc2898DeriveBytes(bytesToHash, salt, 10000);
            return Convert.ToBase64String(byteResult.GetBytes(24));
        }

        public static string GenerateSalt()
        {
            var bytes = new byte[128 / 8];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        //var password = "mypassword123!";
        //var newSalt = GenerateSalt();
        //var hashedPassword = ComputeHash(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytesnewSalt));
    }
}
