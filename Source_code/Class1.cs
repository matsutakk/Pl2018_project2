using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketServer
{
    class Client
    {
        public string username;
        private Socket clientSocket;
        private Thread t;
        private byte[] data = new byte[1024];

        public Client(Socket clientSocket)
        {
            this.clientSocket = clientSocket;
            t = new Thread(ReceiveMessage);
            t.Start();
        }
        /// <summary>
        /// server rece
        /// </summary>
        private void ReceiveMessage()
        {
            while (true)
            {
                int bytes = clientSocket.Receive(data);
                string s = Encoding.UTF8.GetString(data, 0, bytes);
                string[] p = s.Split(' ');
                Console.WriteLine(s);
                switch (p[0])
                {
                    case "RequestLogin":
                        ResponseLogin(p[1]);
                        break;
                    case "RequestChat":
                        Console.WriteLine("receive chat request from" + username + "to" + p[1] + "");
                        ResponseChat(p[1]);
                        break;
                    case "Chat":
                        Transmit(p[1],p[2]);
                        break;
                    case "ChatRoom":
                        BroadCast(p[1]);
                        break;
                }
            }
        }
        /// <summary>
        /// login
        /// </summary>
        /// <param name="s"></param>
        private void ResponseLogin(string s)
        {

            username = s;
            string temp = "LoginSuccessful ";
            foreach (var item in UserInfo.user.Keys)
            {
                temp += item + " ";
            }
            Console.WriteLine("send Ending" );

            temp += "Ending";
            clientSocket.Send(Encoding.UTF8.GetBytes(temp));
            Console.WriteLine("send finish");
            Broad(s);
            
        }
        private void Broad(string s)
        {
            if (UserInfo.user.ContainsKey(s))
            {
                UserInfo.user[s] = true;
            }
            else
            {
                UserInfo.user.Add(s, true);
                Program.BorodCast("NewUser" + " " + s, username);
            }
        }
        /// <summary>
        ///  
        /// </summary>
        /// <param name="s"></param>
        private void ResponseChat(string s)
        {
            if(UserInfo.user.ContainsKey(s))
            {
                if(UserInfo.user[s]==true)
                {
                    foreach (var item in Program.clientList)
                    {
                        if (s == item.username)
                            item.SendChatRequest(username);        
                    }
                }
            }
        }
        public void SendMessage(string s)
        {
            byte[] response = Encoding.UTF8.GetBytes(s);
            clientSocket.Send(response);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public void SendChatRequest(string name)
        {
            byte[] response = Encoding.UTF8.GetBytes("Called"+" "+name);
            this.clientSocket.Send(response);
        }
        public void Transmit(string a,string b)
        {
            Program.FindByName(a).SendMessage("Chat"+" "+username + " " + b);
        }
        public void BroadCast(string s)
        {
            string str="ChatRoom"+" "+s;
            Program.BorodCast(str,username);
        }
    }
}
