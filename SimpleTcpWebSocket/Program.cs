using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleTcpWebSocket
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("### Simple Websocket Request viar REST using TCP ###");
            var result = string.Empty;
            var tcp = new TcpClient("localhost", 8080);
            var stream = tcp.GetStream();
            string headers = "GET /api/v1/parameters HTTP/1.1\r\n" +
                "Host: localhost:8080\r\n" +
                "Accept: */* \r\n" +
                "Connection: Upgrade\r\n" +
                "Upgrade: websocket\r\n" +
                "Sec-WebSocket-Key: SHVwSHVwVHJhbGxhbGFsYQ==\r\n" +
                "Sec-WebSocket-Version: 13\r\n\r\n";
            var headerBuffer = Encoding.ASCII.GetBytes(headers);
            Console.WriteLine("Send Request: ");
            Console.WriteLine(headers);
            await stream.WriteAsync(headerBuffer, 0, headerBuffer.Length);
            
            var data = new byte[1024];
            var responseData = string.Empty;
            int bytes;
            while (stream.CanRead)
            {
                bytes = stream.Read(data, 0, data.Length);
                if (bytes > 0)
                {
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Console.WriteLine("Received: {0}", responseData);
                }
            }
        }
    }    
}
