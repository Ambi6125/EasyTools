using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.SaveFileTools
{
    public class NoStreamException : Exception
    {
        public override string Message => "Observer had no active stream.";
    }

    public class StreamAlreadySetException : Exception
    {
        public override string Message => "Stream was already set.";
    }

    public class FileDoesNotExistException : Exception
    {
        private string fileName;
        public FileDoesNotExistException(string filename)
        {
            fileName = filename;
        }

        public override string Message => $"Couldn't find {fileName} within the observed folder.";
    }

    public class EmptyFileException : Exception
    {
        private string filename;
        public EmptyFileException(string fileName)
        {
            filename = fileName;
        }
        public override string Message => $"{filename} contains no data.";
    }

    public class InvalidTypeResultException : Exception
    {
        public InvalidTypeResultException()
        {
            
        }
        public override string Message => $"The read data could not be transformed into the specified type." +
            "Did you request an incorrect type, or are you referring to a wrong file or old stream?";
    }
}
