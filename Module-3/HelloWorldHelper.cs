using System;

public class HelloWorldHelper
{
    public static string GenerateMessage(string userName)
    {
        return $"{DateTime.Now} Hello, {userName}!";
    }
}