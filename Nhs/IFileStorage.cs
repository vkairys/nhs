using System.IO;

namespace Nhs
{
    public interface IFileStorage
    {
        TextReader ReadData(string path);
    }
}