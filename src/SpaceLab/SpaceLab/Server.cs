using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace SpaceLab
{
    class Server
    {
        Socket server;
        IPHostEntry ipHostInfo;
        IPAddress ipAddress;
        IPEndPoint localEndPoint;
        Socket handler;

        public Server(int port)
        {
            this.ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            this.ipAddress = IPAddress.Parse("127.0.0.1");
            this.localEndPoint = new IPEndPoint(ipAddress, port);
            this.server = new Socket(this.ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine($"listening on {ipAddress}");
        }

        public void Bind()
        {
            this.server.Bind(this.localEndPoint);
            this.server.Listen(10);

            Console.WriteLine("Waiting for a connection...");
            this.handler = this.server.Accept();
        }
        public string WaitForMessage()
        {
            byte[] bytes = new byte[1024];
            string data = null;
            while (true)
            {
                int bytesRec = this.handler.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                if (data.IndexOf("<EOF>") > -1)
                {
                    break;
                }
            }
            return data;
        }

        public void SendMessaage(string msg)
        {
            this.handler.Send(Encoding.ASCII.GetBytes(msg));
        }
    }
}
