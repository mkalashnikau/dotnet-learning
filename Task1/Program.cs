using System;

namespace Task1
{
    internal class Program
    {

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Please enter a string:");
                string userInput = Console.ReadLine();

                try
                {
                    if (string.IsNullOrEmpty(userInput))
                        throw new ArgumentException("Input string is empty!");

                    Console.WriteLine("The first character of entered string is: {0}", userInput[0]);
                }
                catch (ArgumentException exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }
        }
    }
}