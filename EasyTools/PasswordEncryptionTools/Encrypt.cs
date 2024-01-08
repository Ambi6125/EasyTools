using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace EasyTools.PasswordEncryptionTools
{
    public static class Encrypt
    {
        /// <summary>
        /// Return a Hash256 byte[] in string format.
        /// </summary>
        /// <param name="password">The password to encrypt.</param>
        /// <returns>The encrypted password as a string.</returns>
        public static string AsHash256String(string password)
        {
            string encryptedPassword = String.Empty;
            byte[] hashValue;
            UTF8Encoding encoding = new UTF8Encoding();
            using (SHA256 encryptor = SHA256Managed.Create())
            {
                hashValue = encryptor.ComputeHash(encoding.GetBytes(password));
            }
            StringBuilder sb = new StringBuilder(encryptedPassword);
            foreach (byte b in hashValue)
            {
                sb.Append(Convert.ToChar(b));
            }
            return sb.ToString();
        }

        public static byte[] AsHash256(string password)
        {
            string encryptedPassword = String.Empty;
            byte[] hashValue;
            UTF8Encoding encoding = new UTF8Encoding();
            using (SHA256 encryptor = SHA256Managed.Create())
            {
                hashValue = encryptor.ComputeHash(encoding.GetBytes(password));
            }
            return hashValue;
        }

        public static T AccordingTo<T>(IEncryptor<T> encryptor, T password, T salt)
        {
            return encryptor.IEncrypt(password, salt);
        }

        public static string Via(HashHandler hashHandler, string password, string salt)
        {
            return hashHandler(password, salt);
        }
    }
}
