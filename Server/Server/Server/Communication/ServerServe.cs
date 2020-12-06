using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Communication
{
    public class ServerServe
    {
        Socket _serverSocket;
        List<ClientHandler> _clients = new List<ClientHandler>();
        Action<string> _guiUpdater;
        Thread _acceptingThread;

        public ServerServe(string ip, int port, Action<string> guiUpdate)
        {
            _guiUpdater = guiUpdate;
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _serverSocket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
            _serverSocket.Listen(5);
        }

        public void StartAccepting()
        {
            _acceptingThread = new Thread(new ThreadStart(Accept));
            _acceptingThread.IsBackground = true;
            _acceptingThread.Start();
        }

        private void Accept()
        {
            while (_acceptingThread.IsAlive)
            {
                try
                {
                    _clients.Add(new ClientHandler(_serverSocket.Accept(), new Action<string, Socket>(NewMessageReceived)));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Fehler in der Verarbeitung des Servers " + e.Message);
                }
            }
        }

        public void StopAccepting()
        {
            _serverSocket.Close();
            _acceptingThread.Abort(); //abort accepting thread
            //close all client threads and connections
            if (_clients.Count < 1)
                return;

            foreach (var item in _clients)
            {
                item.Close();
            }
            //remove all entries from the list
            _clients.Clear();
        }

        public void DisconnectSpecificClient(string name)
        {
            foreach (var item in _clients)
            {
                if (item.Name.Equals(name))
                {
                    item.Close();
                    _clients.Remove(item); //remove Clienthandler object from list; that works only if we break the foreach after that operation
                    break;
                }
            }
        }

        private void NewMessageReceived(string message, Socket senderSocket)
        {
            //update gui
            _guiUpdater(message);
            //write message to all clients
            foreach (var item in _clients)
            {
                if (item.ClientSocket != senderSocket)
                {
                    item.Send(message);
                }
            }
        }
    }
}
