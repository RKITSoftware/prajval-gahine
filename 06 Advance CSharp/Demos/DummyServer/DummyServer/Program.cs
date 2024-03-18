using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DummyServer
{
    internal class Program
    {
        public static int RequestCount = 0;
        static void Main(string[] args)
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            int port = 45672;

            TcpListener listener = new TcpListener(ipAddress, port);

            listener.Start();

            Console.WriteLine($"Server running on port: {port}");

            while (true)
            {
                Console.WriteLine("Looping");
                TcpClient client = listener.AcceptTcpClient();

                NetworkStream stream = client.GetStream();
                
                StringBuilder requestBuilder = new StringBuilder();
                while (stream.DataAvailable)
                {
                    byte[] buffer = new byte[100];
                    stream.Read(buffer, 0, buffer.Length);
                    requestBuilder.Append(Encoding.UTF8.GetString(buffer));
                }
                
                string request = requestBuilder.ToString();
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");
                Console.WriteLine(++RequestCount);
                Console.WriteLine(request);
                Console.WriteLine();

                string response = string.Empty;

                if (request.Equals(string.Empty))
                {
                    response = "HTTP/1.1 204 No Content";
                    Console.WriteLine("RESPONSE");
                    Console.WriteLine(response);
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------------------"); Console.WriteLine();
                    client.Close();
                    continue;
                }
                else if (request.Substring(0,7).ToLower().Equals("options"))
                {
                    response = $@"HTTP/1.1 200 OK
Access-Control-Allow-Methods: PUT, GET, HEAD, POST, DELETE, OPTIONS
Access-Control-Allow-Origin: https://www.flipkarti.com
Cache-Control: no-cache
Content-Length: 0
Date: Mon, 11 Mar 2024 10:00:20 GMT
Expires: -1
Pragma: no-cache
Server: Microsoft-IIS/10.0
X-Aspnet-Version: 4.0.30319
X-Powered-By: ASP.NET
X-Sourcefiles: =?UTF-8?B?RjpccHJhanZhbC1nYWhpbmVcdGVtcFxGaXJtV2ViQXBpRGVtb1xGaXJtV2ViQXBpRGVtb1xhcGlcbW9ja1xnZXRkYXRh?=";
                    Console.WriteLine("RESPONSE");
                    Console.WriteLine(response);
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------------------"); Console.WriteLine();
                    client.Close();
                    continue;
                }
                else
                {
                    string content = "<!doctype html><html lang=en><head><link rel=\"icon\" href=\"data:;base64,iVBORw0KGgo=\"><meta charset=utf-8>\r\n<title>PNG</title>\r\n</head>\r\n<body>\r\n<p>Hii, Prajval from server</p>\r\n</body>\r\n</html>";

                    int contentLength = Encoding.UTF8.GetByteCount(content);

                    response = $@"HTTP/1.1 200 OK
Access-Control-Allow-Origin: https://www.flipkarti.com
Content-Type: text/html; charset=utf-8
Content-Length: {contentLength}

{content}";
                    Console.WriteLine("RESPONSE");
                    Console.WriteLine(response);
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------------------"); Console.WriteLine();

                    byte[] responseByte = Encoding.UTF8.GetBytes(response);
                    stream.Write(responseByte);

                    client.Close();
                }
            }
        }
    }
}