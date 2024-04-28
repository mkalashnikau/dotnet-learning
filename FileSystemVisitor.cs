using System;
using System.Collections.Generic;
using System.IO;

public class FileSystemVisitor
{
    public event EventHandler<FileSystemInfoEventArgs> Start;
    public event EventHandler<FileSystemInfoEventArgs> Finish;
    public event EventHandler<FileSystemInfoEventArgs> FileFound;
    public event EventHandler<FileSystemInfoEventArgs> DirectoryFound;
    public event EventHandler<FileSystemInfoEventArgs> FilteredFileFound;
    public event EventHandler<FileSystemInfoEventArgs> FilteredDirectoryFound;

    private readonly string _rootDirectory;
    private readonly Predicate<string> _filter;

    public FileSystemVisitor(string rootDirectory, Predicate<string> filter = null)
    {
        _rootDirectory = rootDirectory;
        _filter = filter;
    }

    public IEnumerable<string> Traverse()
    {
        OnStart(new FileSystemInfoEventArgs(_rootDirectory));

        foreach (var item in SafeEnumerateFileSystemEntries(_rootDirectory, "*", SearchOption.AllDirectories))
        {
            bool isFile = File.Exists(item);
            bool isDirectory = Directory.Exists(item);
            bool isMatched = _filter?.Invoke(item) ?? false;

            var args = new FileSystemInfoEventArgs(item, isMatched);

            if (isFile)
            {
                FileFound?.Invoke(this, args);

                if (isMatched)
                {
                    FilteredFileFound?.Invoke(this, args);
                }
            }
            else if (isDirectory)
            {
                DirectoryFound?.Invoke(this, args);

                if (isMatched)
                {
                    FilteredDirectoryFound?.Invoke(this, args);
                }
            }

            if (args.Action == ActionType.Abort)
            {
                yield break;
            }
            else if (args.Action == ActionType.Exclude && isMatched || _filter != null && !isMatched)
            {
                continue;
            }

            yield return item;
        }

        OnFinish(new FileSystemInfoEventArgs(_rootDirectory));
    }

    private IEnumerable<string> SafeEnumerateFileSystemEntries(
        string rootDirectory, string searchPattern, SearchOption searchOptions)
    {
        var dirsToEnumerate = new Queue<string>();
        dirsToEnumerate.Enqueue(rootDirectory);

        while (dirsToEnumerate.Count > 0)
        {
            var currentDir = dirsToEnumerate.Dequeue();
            IEnumerable<string> entries;
            try
            {
                entries = Directory.EnumerateFileSystemEntries(currentDir, searchPattern);
            }
            catch (UnauthorizedAccessException)
            {
                continue;
            }

            foreach (var entry in entries)
            {
                yield return entry;
                if (Directory.Exists(entry) && searchOptions == SearchOption.AllDirectories)
                {
                    dirsToEnumerate.Enqueue(entry);
                }
            }
        }
    }

    protected virtual void OnStart(FileSystemInfoEventArgs e)
    {
        EventHandler<FileSystemInfoEventArgs> handler = Start;
        handler?.Invoke(this, e);
    }

    protected virtual void OnFinish(FileSystemInfoEventArgs e)
    {
        EventHandler<FileSystemInfoEventArgs> handler = Finish;
        handler?.Invoke(this, e);
    }
}

public enum ActionType
{
    Continue,
    Exclude,
    Abort
}

public class FileSystemInfoEventArgs : EventArgs
{
    public string Path { get; }
    public ActionType Action { get; set; }
    public bool IsMatched { get; }

    public FileSystemInfoEventArgs(string path, bool isMatched = false)
    {
        Path = path;
        IsMatched = isMatched;
        Action = ActionType.Continue;
    }
}