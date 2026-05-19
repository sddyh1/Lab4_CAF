using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.Adapters
{
    public class CloudStorageAdapter : IFileSystemAdapter
    {
        private Dictionary<string, string> _storage = new Dictionary<string, string>();

        public List<string> ListContents(string path)
        {
            Console.WriteLine($"Облако: запрос содержимого '{path}'");
            return _storage.Keys.Where(k => k.StartsWith(path)).ToList();
        }

        public string Read(string path)
        {
            _storage.TryGetValue(path, out var content);
            Console.WriteLine($"Облако: чтение '{path}'");
            return content ?? string.Empty;
        }

        public void Write(string path, string content)
        {
            _storage[path] = content;
            Console.WriteLine($"Облако: запись в '{path}'");
        }

        public void Delete(string path)
        {
            _storage.Remove(path);
            Console.WriteLine($"Облако: удаление '{path}'");
        }
    }
}