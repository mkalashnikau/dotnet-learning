using System.Net;
using System.Text;

class Program
{
    static HttpListener listener;

    static void Main(string[] args)
    {
        listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:8888/");
        listener.Start();

        Console.WriteLine("Listening...");

        while (true)
        {
            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            var path = request.RawUrl.Replace("/", "");

            switch (path)
            {
                case "MyName":
                    var name = Encoding.UTF8.GetBytes("Your name"); //Change 'Your name' to your name
                    response.OutputStream.Write(name, 0, name.Length);
                    break;
                case "Information":
                    response.StatusCode = 100;
                    break;
                case "Success":
                    response.StatusCode = 200;
                    break;
                case "Redirection":
                    response.StatusCode = 300;
                    break;
                case "ClientError":
                    response.StatusCode = 400;
                    break;
                case "ServerError":
                    response.StatusCode = 500;
                    break;
                default:
                    response.StatusCode = 404;
                    break;
            }
            response.Close();
        }
    }
}