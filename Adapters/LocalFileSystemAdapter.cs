using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1.Adapters
{
    public class LocalFileSystemAdapter : IFileSystemAdapter
    {
        public List<string> ListContents(string path)
        {
            var result = new List<string>();
            if (Directory.Exists(path))
            {
                result.AddRange(Directory.GetDirectories(path));
                result.AddRange(Directory.GetFiles(path));
            }
            return result;
        }

        public string Read(string path)
        {
            if (System.IO.File.Exists(path))
                return System.IO.File.ReadAllText(path);
            return string.Empty;
        }

        public void Write(string path, string content)
        {
            System.IO.File.WriteAllText(path, content);
            Console.WriteLine($"Локально записано: {path}");
        }

        public void Delete(string path)
        {
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
            else if (Directory.Exists(path))
                Directory.Delete(path, true);
            Console.WriteLine($"Локально удалено: {path}");
        }
    }
}