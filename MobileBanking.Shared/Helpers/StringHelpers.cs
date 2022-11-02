using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MobileBanking.Shared.Helpers
{
    public static class StringHelpers
    {
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length == 10 && phoneNumber.StartsWith("07"))
                return true;
            else
                return false;
        }

        public static bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidName(string name)
        {
            Regex r = new Regex("^[a-zA-Z ]+$");
            if (r.IsMatch(name))
                return true;
            else
                return false;
        }

        public static string GenerateRandomNumber(long min= 1000000000, long max= 9999999999)
        {
            Random _random = new Random();
            long result = _random.Next((Int32)(min >> 32), (Int32)(max >> 32));
            result = (result << 32);
            result = result | (long)_random.Next((Int32)min, (Int32)max);
            return result.ToString();
        }
    }
}
