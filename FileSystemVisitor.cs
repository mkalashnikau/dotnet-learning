using System;
using System.Collections.Generic;
using System.IO;

public class FileSystemVisitor
{
    private readonly string _rootFolder;
    private readonly Predicate<string> _filter;

    public FileSystemVisitor(string rootFolder, Predicate<string> filter = null)
    {
        _rootFolder = rootFolder;
        _filter = filter;
    }

    public IEnumerable<string> Traverse()
    {
        Stack<string> stack = new Stack<string>();
        stack.Push(_rootFolder);

        while (stack.Count > 0)
        {
            string currentDir = stack.Pop();

            bool shouldReturnCurrentDir = _filter == null || _filter(currentDir);

            if (shouldReturnCurrentDir)
            {
                yield return currentDir;
            }

            string[] directories = null;
            string[] files = null;

            try
            {
                directories = Directory.GetDirectories(currentDir);
                files = Directory.GetFiles(currentDir);
            }
            catch (UnauthorizedAccessException)
            {
                continue;
            }

            foreach (var directory in directories)
            {
                stack.Push(directory);
            }

            foreach (var file in files)
            {
                bool shouldReturnFile = _filter == null || _filter(file);

                if (shouldReturnFile)
                {
                    yield return file;
                }
            }
        }
    }
}