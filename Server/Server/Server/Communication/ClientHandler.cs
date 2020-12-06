using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Communication
{
    //nur in der eigenen Assembly aufrufbar
    internal class ClientHandler
    {
        private Action<string, Socket> _action;
        private byte[] _buffer = new byte[512];
        private Thread _clientReceiveThread;

        public string Name { get; set; }

        public Socket ClientSocket{get; private set;}

        public ClientHandler(Socket socket, Action<string, Socket> action)
        {
            this.ClientSocket = socket;
            _action = action;
            _clientReceiveThread = new Thread(Receive);
            _clientReceiveThread.Start();
        }

        private void Receive()
        {
            string message = "";
            while (message != "@quit")
            {
                int length = ClientSocket.Receive(_buffer);
                message = Encoding.UTF8.GetString(_buffer, 0, length);
                if (Name == null && message.Contains(":")){
                    Name = message.Split(':')[0];
                }
                //Gui informieren
                _action(message, ClientSocket);
            }
            Close();
        }

        public void Send(string message)
        {
            ClientSocket.Send(Encoding.UTF8.GetBytes(message));
        }

        public void Close()
        {
            Send("@quit"); //sends endmessage to client 
            ClientSocket.Close(1); //disconnects
            _clientReceiveThread.Abort(); //abort client threads
        }

    }
}
