using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;
using EasyTools.ConversionTools;
using EasyTools.ExtensionMethods;

namespace EasyTools.SaveFileTools
{
    public delegate IList<T> DataReader<T>(string location);

    public enum AutoPathing { OFF = 0, ON = 1 }
    public enum FileStatus { Created, Existant, NonExistant }
    public sealed class FileObserver : IDisposable, IEquatable<FileObserver>
    {
        private readonly string folderLocation;
        private FileStream fs = null;

        public string ObservedFolder
        {
            get
            {
                return folderLocation;
            }
        }
        public FileStream Stream
        {
            get
            {
                if (fs != null)
                {
                    return fs;
                }
                return null;
            }
        }

        public FileObserver(string folderLocation)
        {
            this.folderLocation = folderLocation;
            //fs = new FileStream(folderLocation, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }

        ~FileObserver()
        {
            Dispose();
        }

        private string SpecifyFile(string file)
        {
            StringBuilder sb = new StringBuilder(folderLocation);
            sb.Append(@"\" + file);
            return sb.ToString();
        }

        public void OpenStream(string fileName)
        {
            OpenStream(fileName, AutoPathing.ON);
        }

        public void OpenStream(string fileName, FileAccess access)
        {
            OpenStream(fileName, AutoPathing.ON, access);
        }

        /// <summary>
        /// Opens the observer stream to a file within the folder.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="pathing">ON: The system automatically writes the full path. OFF: Manually write the full path. The full path has to be provided in the parameter if this is used.</param>
        public void OpenStream(string fileName, AutoPathing pathing)
        {
            string fileLocation = fileName;
            int autoPath = (int)pathing;

            if (Converter.IntToBool((int)pathing))
            {
                fileLocation = SpecifyFile(fileName);
            }

            fs = new FileStream(fileLocation, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }

        public void OpenStream(string fileName, AutoPathing pathing, FileAccess fileAccess)
        {
            if (fs != null)
            {
                throw new StreamAlreadySetException();
            }
            string fileLocation = fileName;

            if (Converter.IntToBool((int)pathing))
            {
                fileLocation = SpecifyFile(fileName);
            }

            fs = new FileStream(fileLocation, FileMode.OpenOrCreate, fileAccess);

        }

        public void EndStream()
        {
            fs.Close();
            fs = null;
        }



        public IReadOnlyList<ResultType> ReadBinaryFromFile<ResultType>()
        {
            List<ResultType> results = new List<ResultType>();
            try
            {
                object[] readResult;
                using (fs)
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    readResult = (object[])bf.Deserialize(fs);
                }
                foreach (object obj in readResult)
                {
                    results.Add((ResultType)obj);
                }
            }
            catch (InvalidCastException)
            {
                throw new InvalidTypeResultException();
            }
            catch (System.Runtime.Serialization.SerializationException)
            {
                throw new EmptyFileException(Stream.Name);
            }
            return results;
        }

        public void OverWriteFileWithBinaryData<InputType>(IEnumerable<InputType> input)
        {
            BinaryFormatter bf = new BinaryFormatter();
            if (fs == null)
            {
                throw new NoStreamException();
            }
            List<InputType> types = input.ToList();
            bf.Serialize(fs, types);
        }

        private string FusePath(string file, AutoPathing autoPathing)
        {
            if (autoPathing == AutoPathing.ON)
            {
                return $@"{folderLocation}\{file}";
            }
            return file;
        }

        public FileStatus CreateNewFile(string filename)
        {
            return CreateNewFile(filename, AutoPathing.ON);
        }
        public FileStatus CreateNewFile(string filename, AutoPathing autoPathing)
        {
            string file = FusePath(filename, autoPathing);
            if (File.Exists(file))
            {
                return FileStatus.Existant;
            }
            else
            {
                File.Create(file);
                return FileStatus.Created;
            }
        }



        public FileStatus CheckFileStatus(string fileName, AutoPathing autoPathing)
        {
            string file = FusePath(fileName, autoPathing);
            if (File.Exists(file))
            {
                return FileStatus.Existant;
            }
            else
            {
                return FileStatus.NonExistant;
            }
        }

        public bool ContainsFile(string filename)
        {
            return ContainsFile(filename, AutoPathing.ON);
        }

        public bool ContainsFile(string fileName, AutoPathing autoPathing)
        {
            switch (CheckFileStatus(fileName, autoPathing)) //Uses a switch for easy extension in case the enum ever extends
            {
                case FileStatus.NonExistant:
                    return false;
                default:
                    return true;
            }
        }


        /// <summary>
        /// Determines if the observed folder contains a certain file.
        /// </summary>
        /// <param name="fileName">Name of the file</param>
        /// <param name="createFileIfFalse">If this is true, the file will be created if it does not exist yet.</param>
        /// <param name="autoPathing">Automatically constructs the file address according to observed folder.</param>
        /// <returns></returns>
        public FileStatus CheckFileStatus(string fileName, bool createFileIfNonExistant, AutoPathing autoPathing)
        {
            string file = FusePath(fileName, autoPathing); 
            if (File.Exists(file))
            {
                return FileStatus.Existant;
            }
            if (createFileIfNonExistant)
            {
                File.Create(file);
                return FileStatus.Created;
            }
            return FileStatus.NonExistant;
        }

        public void Dispose()
        {
            if(fs != null)
            {
                fs.Dispose();
            }
        }

        public bool Equals(FileObserver other)
        {
            if(ObservedFolder == other.ObservedFolder)
            {
                return true;
            }
            return false;
        }
    }

}
