using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client.Communication
{
    class Client
    {
        byte[] buffer = new byte[512];
        Socket _clientSocket;
        Action<string> _messageInformer;
        Action _abortInformer;
        public Client(string ip, int port, Action<string> messageInformer, Action abortInformer)
        {
            _abortInformer = abortInformer;
            _messageInformer = messageInformer;
            var client = new TcpClient();
            client.Connect(IPAddress.Parse(ip), port);
            _clientSocket = client.Client;
            StartReceiving();
        }

        public void StartReceiving()
        {
            Task.Factory.StartNew(Receive);
        }

        public void Receive()
        {
            string message = "";
            while(message != "@quit")){
                int length = _clientSocket.Receive(buffer);
                message = Encoding.UTF8.GetString(buffer, 0, length);
                _messageInformer(message);
            }
        }

        public void Send(string message)
        {
            if (_clientSocket != null)
                _clientSocket.Send(Encoding.UTF8.GetBytes(message));
        }

        public void close()
        {
            _clientSocket.Close();
            _abortInformer();
        }
    }
}
