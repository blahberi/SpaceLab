using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Client
{
    class Program
    {
        public static void Main(String[] args)
        {
            Console.WriteLine("port:");
            int port = int.Parse(Console.ReadLine());
            Console.WriteLine("message:");
            string message = Console.ReadLine();

            Console.WriteLine("do you want EOF?");
            bool useEOF = false;
            if (Console.ReadLine() == "yes")
            {
                useEOF = true;
            }

            Client client = new Client(port);
            client.Connect();
            client.SendMessage(message, useEOF);
            Console.ReadLine();
        }
    }
}
