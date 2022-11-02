using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MobileBanking.API.Helpers
{
    public static class PhoneListingHelper
    {
        public static bool CheckIsBlacklisted(string phoneNumber)
        {
            return checkFileContainsPhoneNumber("BlackList.txt",phoneNumber);
        }

        public static bool CheckIsWhitelisted(string phoneNumber)
        {
            return checkFileContainsPhoneNumber("WhiteList.txt", phoneNumber);
        }

        private static bool checkFileContainsPhoneNumber(string fileName, string phoneNumber)
        {
            string filePath = Path.Combine("StaticFiles", fileName);
            using (StreamReader sr = new StreamReader(filePath))
            {
                string contents = sr.ReadToEnd();
                if (contents.Contains(phoneNumber))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
