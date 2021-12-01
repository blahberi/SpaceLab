using System;

namespace SpaceLab
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("enter the COM of the arduino");
            int serial_com = int.Parse(Console.ReadLine());
            Console.WriteLine("enter the Serial Port for the arduino");
            int serial_port = int.Parse(Console.ReadLine());
            SerialCom serialCom = new SerialCom(serial_com, serial_port);
            serialCom.Open();


            Console.WriteLine("enter the port for the socket");
            int socket_port = int.Parse(Console.ReadLine());
            Server server = new Server(socket_port);
            server.Bind();
            Console.WriteLine("connected!");

            Console.WriteLine("waiting for message...");
            string msg = server.WaitForMessage();
            if (msg == "gaming!<EOF>")
            {
                Console.WriteLine($"Sending message to arduino through COM{serial_com} port 9600");
                serialCom.Send("gaming!");
            }
            else
            {
                Console.WriteLine(msg);
                Console.WriteLine("failed...");
            }
            Console.ReadLine();
        }
    }
}
