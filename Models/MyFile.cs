using System;

namespace ConsoleApp1.Models
{
    public class MyFile : IFileSystemNode
    {
        public string Name { get; }
        private long _size;

        public MyFile(string name, long size)
        {
            Name = name;
            _size = size;
        }

        public long GetSize() => _size;

        public void Delete()
        {
            Console.WriteLine($"Файл '{Name}' удалён.");
        }

        public void Copy(string destinationPath)
        {
            Console.WriteLine($"Файл '{Name}' скопирован в '{destinationPath}'.");
        }

        public void Display(int depth = 0)
        {
            Console.WriteLine(new string(' ', depth * 2) + $"- {Name} ({_size} байт)");
        }
    }
}