using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.PasswordEncryptionTools
{
    public class SHA512Generator : IEncryptor<string>
    {
        public string IEncrypt(string password, string salt)
        {
            IEnumerable<char> concatenation = password.Concat(salt);
            byte[] bytes = Encoding.UTF8.GetBytes(concatenation.ToArray());
            using (SHA512 hasher = SHA512.Create())
            {
                byte[] hashedBytes = hasher.ComputeHash(bytes);

                StringBuilder resultBuilder = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    resultBuilder.Append(b.ToString("X2"));
                }
                return resultBuilder.ToString();
            }
        }
    }
}
