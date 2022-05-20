using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCP_Client
{
    internal interface IClient
    {
        Task StartAsync(string server, int port);

        void SendMessage(string message, NetworkStream stream);

        void ReciveMessage(NetworkStream stream);
       
    }
}
