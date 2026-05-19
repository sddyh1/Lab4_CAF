using System;
using ConsoleApp1.Models;
using ConsoleApp1.Adapters;

namespace ConsoleApp1.Facade
{
    public class FileManagerFacade
    {
        private IFileSystemAdapter _source;
        private IFileSystemAdapter _target;

        public FileManagerFacade(IFileSystemAdapter source, IFileSystemAdapter target)
        {
            _source = source;
            _target = target;
        }

        public void SyncFolders(string sourcePath, string targetPath)
        {
            Console.WriteLine($"\n=== Синхронизация из '{sourcePath}' в '{targetPath}' ===");
            var sourceTree = BuildTree(sourcePath, _source);
            CopyTree(sourceTree, targetPath, _target);
            Console.WriteLine("Синхронизация завершена.");
        }

        public void BackupFolder(string folderPath, string backupRoot)
        {
            string backupName = $"{backupRoot}/Backup_{DateTime.Now:yyyyMMdd_HHmmss}";
            Console.WriteLine($"\n=== Создание резервной копии '{folderPath}' в '{backupName}' ===");
            var tree = BuildTree(folderPath, _source);
            CopyTree(tree, backupName, _target);
            Console.WriteLine("Резервное копирование завершено.");
        }

        private IFileSystemNode BuildTree(string path, IFileSystemAdapter adapter)
        {
            var dir = new MyDirectory(System.IO.Path.GetFileName(path));
            var contents = adapter.ListContents(path);
            foreach (var item in contents)
            {
                dir.Add(new MyFile(item, 1024));
            }
            return dir;
        }

        private void CopyTree(IFileSystemNode node, string destPath, IFileSystemAdapter adapter)
        {
            if (node is MyFile file)
            {
                string content = _source.Read(file.Name);
                adapter.Write(destPath + "/" + file.Name, content);
            }
            else if (node is MyDirectory dir)
            {
                Console.WriteLine($"Создаётся папка: {destPath}");
                foreach (var child in ((MyDirectory)node).GetChildren())
                {
                    CopyTree(child, destPath + "/" + child.Name, adapter);
                }
            }
        }
    }

}
