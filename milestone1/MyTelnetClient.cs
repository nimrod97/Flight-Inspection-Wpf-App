using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace milestone1
{
    class MyTelnetClient : ITelnetClient
    {
        TcpClient client;
        NetworkStream stream;
        public MyTelnetClient()
        {
            client = new TcpClient();
        }
        public void connect(string ip, int port)
        {
            try
            {
                client = new TcpClient(ip, port);
                stream = client.GetStream();
            }
            catch (Exception e)
            {
                return;
            }
        }

        public string read()
        {
            throw new NotImplementedException();
        }

        public void write(string line)
        {
            if (client != null && client.Connected)
            {
                byte[] messageSent = Encoding.ASCII.GetBytes(line + "\r\n");
                stream.Write(messageSent, 0, messageSent.Length);
            }
        }

        public void disconnect()
        {
            client.Close();
        }
    }
}
