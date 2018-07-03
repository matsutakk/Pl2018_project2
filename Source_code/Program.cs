using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SocketServer
{
    class Program
    {
        public static List<Client> clientList = new List<Client>();
        static void Main(string[] args)
        {
            Socket tcpServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpServer.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6789));
            tcpServer.Listen(100);
            while (true)
            {
                Socket clientSocket = tcpServer.Accept();
                Console.WriteLine("one client connected");
                Client client = new Client(clientSocket);
                clientList.Add(client);
            }
        }
        /// <summary>
        /// 广播
        /// </summary>
        /// <param name="s"></param>
        public static void BorodCast(string s,string username)
        {
            foreach (var item in clientList)
            {
                if (item.username != username)
                {
                    byte[] data = Encoding.UTF8.GetBytes(s);
                    item.SendMessage(s);
                }
            }
        }
        public static Client FindByName(string name)
        {
            foreach (var item in clientList)
            {
                if (item.username == name)
                    return item;
            }
            return null;
        }
    }
}
