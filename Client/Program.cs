using System.Net;
using System.Text;

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
}
