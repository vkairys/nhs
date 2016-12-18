using System.IO;

namespace Nhs
{
    public class FileStorage : IFileStorage
    {
        public StreamReader ReadData(string path)
        {
            return File.OpenText(path);
        }
    }
}