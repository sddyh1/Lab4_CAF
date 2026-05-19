using System;
using ConsoleApp1.Models;
using ConsoleApp1.Adapters;
using ConsoleApp1.Facade;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Демонстрация паттернов Composite, Adapter, Facade ===\n");

            var root = new MyDirectory("корень");
            var docs = new MyDirectory("Документы");
            var pics = new MyDirectory("Изображения");

            var file1 = new MyFile("resume.txt", 120);
            var file2 = new MyFile("photo.jpg", 2048);
            var file3 = new MyFile("notes.txt", 45);

            docs.Add(file1);
            docs.Add(file3);
            pics.Add(file2);
            root.Add(docs);
            root.Add(pics);

            Console.WriteLine("Структура файлов и папок:");
            root.Display();

            Console.WriteLine($"\nОбщий размер: {root.GetSize()} байт");

            Console.WriteLine("\n--- Копирование папки ---");
            root.Copy("/backup");

            IFileSystemAdapter local = new LocalFileSystemAdapter();
            IFileSystemAdapter cloud = new CloudStorageAdapter();

            Console.WriteLine("\n--- Работа через адаптеры ---");
            local.Write("test.txt", "Hello, local!");
            cloud.Write("cloud_note.txt", "Hello, cloud!");

            Console.WriteLine($"Чтение из local: {local.Read("test.txt")}");
            Console.WriteLine($"Чтение из cloud: {cloud.Read("cloud_note.txt")}");

            var facade = new FileManagerFacade(local, cloud);
            facade.SyncFolders("C:/temp/source", "cloud_backup");
            facade.BackupFolder("C:/temp/data", "cloud_backups");

            local.Delete("test.txt");
            cloud.Delete("cloud_note.txt");

            Console.WriteLine("\nРабота программы завершена.");
        }
    }
}