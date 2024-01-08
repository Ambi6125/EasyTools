using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.LoginTools
{
    public class EncryptionMethod
    {
        public delegate string StringEncryptor(string preEncryptionString);
        public delegate byte[] ByteArrayFromStringEncryptor(string preEncryptionString);
        public delegate byte[] PureByteArrayEncryptor(byte[] byteArray);

        public delegate TResult Encryptor<in TInput, out TResult>(TInput input);
    }
}
