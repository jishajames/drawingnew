//using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MyClassLibrarynew.Helpers
{
    public static class PasswordHelper
    {
        private static string key = "w4wiua21e25c4cccr13h0rtiwrruiaw5";
        public static string DecryptPassword(string password)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(password);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        public static bool VerifyEncryptedPassword(string storedPassword, string passwordToCheck)
        {
            var passwordDecrypted = DecryptPassword(storedPassword);
            if (string.Compare(passwordDecrypted, passwordToCheck) == 0)
                return true;

            return false;
        }
    }
}
