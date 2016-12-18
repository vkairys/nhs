using System.IO;

namespace Nhs
{
    public interface IFileStorage
    {
        StreamReader ReadData(string path);
    }
}