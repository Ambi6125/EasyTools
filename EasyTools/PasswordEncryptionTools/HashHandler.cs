using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.PasswordEncryptionTools
{
    public delegate string HashHandler(string password, string salt);
}
