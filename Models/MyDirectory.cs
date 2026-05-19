using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.Models
{
    public class MyDirectory : IFileSystemNode
    {
        public string Name { get; }
        private List<IFileSystemNode> _children = new List<IFileSystemNode>();

        public MyDirectory(string name)
        {
            Name = name;
        }

        public void Add(IFileSystemNode node) => _children.Add(node);
        public void Remove(IFileSystemNode node) => _children.Remove(node);
        public List<IFileSystemNode> GetChildren() => _children;

        public long GetSize() => _children.Sum(c => c.GetSize());

        public void Delete()
        {
            foreach (var child in _children) child.Delete();
            _children.Clear();
            Console.WriteLine($"Папка '{Name}' удалена.");
        }

        public void Copy(string destinationPath)
        {
            Console.WriteLine($"Папка '{Name}' скопирована в '{destinationPath}' (со всем содержимым).");
            foreach (var child in _children)
                child.Copy(destinationPath + "/" + child.Name);
        }

        public void Display(int depth = 0)
        {
            Console.WriteLine(new string(' ', depth * 2) + $"+ {Name}/ (размер: {GetSize()} байт)");
            foreach (var child in _children) child.Display(depth + 1);
        }
    }
}