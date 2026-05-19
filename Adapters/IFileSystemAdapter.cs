using System.Collections.Generic;

namespace ConsoleApp1.Adapters
{
    public interface IFileSystemAdapter
    {
        List<string> ListContents(string path);
        string Read(string path);
        void Write(string path, string content);
        void Delete(string path);
    }
}