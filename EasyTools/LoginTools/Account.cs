using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTools.ObjectManagingTools;
using EasyTools.PasswordEncryptionTools;

namespace EasyTools.LoginTools
{
    public class TemplateAccount : ManagableObject, IManagable<string>
    {
        private string _password;

        /// <summary>
        /// Create an account instance. The username will be considered as its identifier.
        /// </summary>
        /// <param name="username">The account's username.</param>
        public TemplateAccount(string username, string password) : base(username)
        {
            this._password = Encrypt.AsHash256String(password);
        }

        public TemplateAccount(string username, string password, IEncryptor encryptor) : base(username)
        {
            _password = (string)encryptor.IEncrypt(password);
        }

        public  TemplateAccount(string username, string password, EncryptionMethod.StringEncryptor encryptor) : base(username)
        {
            _password = encryptor(password);
        }

        public new string GetIdentifier()
        {
            return Identifier;
        }

        public bool PasswordMatches(string password)
        {
            return password == _password;
        }

        public void ChangePassword(EncryptionMethod.StringEncryptor stringEncryptor, string newPassword)
        {
            _password = stringEncryptor(newPassword);
        }

        public void ChangeUsername(string newUsername)
        {
            Identifier = newUsername;
        }
    }
}
