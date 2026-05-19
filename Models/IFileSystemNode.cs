namespace ConsoleApp1.Models
{
    public interface IFileSystemNode
    {
        string Name { get; }
        long GetSize();
        void Delete();
        void Copy(string destinationPath);
        void Display(int depth = 0);
    }
}