using System.IO;

namespace Nhs
{
    public class FileStorage : IFileStorage
    {
        public TextReader ReadData(string path)
        {
            return File.OpenText(path);
        }
    }
}