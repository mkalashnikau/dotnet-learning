using System;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide a username.");
            return;
        }

        string userName = args[0];
        string message = HelloWorldHelper.GenerateMessage(userName);
        Console.WriteLine(message);
    }
}