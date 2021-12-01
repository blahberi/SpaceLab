using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Client
    {
        IPHostEntry ipHostInfo;
        IPAddress ipAddress;
        IPEndPoint retmoteEndPoint;
        Socket client;

        public Client(int port)
        {
            this.ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            this.ipAddress = IPAddress.Parse("127.0.0.1");
            this.retmoteEndPoint = new IPEndPoint(ipAddress, port);
            this.client = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect()
        {
            this.client.Connect(this.retmoteEndPoint);
        }

        public void SendMessage(string message, bool useEOF=false)
        {
            if (useEOF)
            {
                message += "<EOF>";
            }
            byte[] msg_bytes = Encoding.ASCII.GetBytes(message);
            this.client.Send(msg_bytes);
        }
    }
}
