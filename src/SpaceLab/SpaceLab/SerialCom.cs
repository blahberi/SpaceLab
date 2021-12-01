using System;
using System.IO.Ports;

namespace SpaceLab
{
    class SerialCom
    {
        private SerialPort serialPort;
        public SerialCom(int com, int port)
        {
            this.serialPort = new SerialPort();
            this.serialPort.PortName = $"COM{com}";
            this.serialPort.BaudRate = port;
        }
        public void Open()
        {
            this.serialPort.Open();
        }
        public void Send(string message)
        {
            this.serialPort.WriteLine(message);
        }
    }
}
// a44Lou4iMsmAM7Pl/LP6h4Ic0AnvDsquy95ZW6S6mPxM+Qvcx3XEFNe79uso9ZGr