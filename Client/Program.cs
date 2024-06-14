using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static HttpClient client = new HttpClient();

    static void Main(string[] args)
    {
        CallMyName().GetAwaiter().GetResult();
        GetStatusCode("Information").GetAwaiter().GetResult();
        GetStatusCode("Success").GetAwaiter().GetResult();
        GetStatusCode("Redirection").GetAwaiter().GetResult();
        GetStatusCode("ClientError").GetAwaiter().GetResult();
        GetStatusCode("ServerError").GetAwaiter().GetResult();

        GetNameByHeader().GetAwaiter().GetResult();
        GetNameByCookies().GetAwaiter().GetResult();
    }

    static async Task CallMyName()
    {
        HttpResponseMessage response = await client.GetAsync("http://localhost:8888/MyName");

        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            Console.WriteLine("I am " + content);
        }
    }

    static async Task GetStatusCode(string path)
    {
        HttpResponseMessage response = await client.GetAsync($"http://localhost:8888/{path}");

        Console.WriteLine($"{path}:{response.StatusCode}");
    }

    static async Task GetNameByHeader()
    {
        HttpResponseMessage response = await client.GetAsync("http://localhost:8888/MyNameByHeader");

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Name from header: " + response.Headers.GetValues("X-MyName").FirstOrDefault());
        }
    }

    static async Task GetNameByCookies()
    {
        HttpResponseMessage response = await client.GetAsync("http://localhost:8888/MyNameByCookies");

        if (response.IsSuccessStatusCode)
        {
            var cookies = response.Headers.GetValues("Set-Cookie");
            string myName = cookies.Select(c => c.Split('='))
                                    .Where(c => c[0] == "MyName")
                                    .Select(c => c[1])
                                    .FirstOrDefault();
            Console.WriteLine("Name from cookie: " + myName);
        }
    }
}