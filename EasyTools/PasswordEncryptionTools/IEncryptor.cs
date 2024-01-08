using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace EasyTools.PasswordEncryptionTools
{
    public interface IEncryptor
    {
        object IEncrypt(object input);
    }
    //public interface IEncryptor<in T, out TResult> : IEncryptor
    //{
    //    TResult IEncrypt(T password); 
    //}

    public interface IEncryptor<T>
    {
        T IEncrypt(T password, T salt);
    }
}
