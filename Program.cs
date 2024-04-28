using System;

public class Program
{
    public static void Main(string[] args)
    {
        FileSystemVisitor visitor = new FileSystemVisitor("C:\\", fileOrFolder => fileOrFolder.EndsWith(".txt"));
        foreach (string fileOrFolder in visitor.Traverse())
        {
            Console.WriteLine(fileOrFolder);
        }
    }
}