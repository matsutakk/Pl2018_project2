using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Net;
using System.IO;
using System.Net.Sockets;



public class Server : MonoBehaviour {

    private List<ServerClient> clients; // connected List
    private List<ServerClient> disconnectedList;

    public int port = 6321;
    private TcpListener server;
    private bool serverStarted;

    private void Start()
    {
        clients = new List<ServerClient>();
        disconnectedList = new List<ServerClient>();

        try
        {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();

            StartListening();
            serverStarted = true;
            Debug.Log("Server has been started on port: " + port.ToString());
        }
        catch(Exception e)
        {
            Debug.Log("Socket error: " + e.Message);
        }

    }

    private void Update()
    {
        if (!serverStarted)
            return;

        try
        {
            foreach (ServerClient c in clients)
            {
                
                // Is the client still connected ?
                if (!IsConnected(c.tcp))
                {
                    c.tcp.Close();
                    disconnectedList.Add(c);
                    continue;
                }
                // Check for message from the client
                else
                {
                    NetworkStream s = c.tcp.GetStream();
                    if (s.DataAvailable)
                    {
                        StreamReader reader = new StreamReader(s, true);
                        string data = reader.ReadLine();

                        if (data != null)
                        {
                            OnIncomingData(c, data);
                        }
                    }
                }

                for (int i = 0; i < (disconnectedList.Count - 1); i++)
                {
                    Broadcast(disconnectedList[i].clientName + " has disconnected", clients);
                    clients.Remove(disconnectedList[i]);
                    disconnectedList.RemoveAt(i);
                }

            }
        }
        catch(InvalidOperationException e) 
        {
            Debug.Log(e.Message);
            Debug.Log("please ignore above erorrs");
        }

    }

    private void StartListening(){
        server.BeginAcceptTcpClient(AcceptTcpClient, server);
    }

    private bool IsConnected(TcpClient c)
    {

        try
        {
            if (c != null && c.Client != null && c.Client.Connected)
            {
                if (c.Client.Poll(0, SelectMode.SelectRead))
                {
                    return !(c.Client.Receive(new byte[1], SocketFlags.Peek) == 0);
                }

                return true;
            }
            else
                return false;
        }
        catch
        {
            return false;
        }
    }
    
    private void AcceptTcpClient(IAsyncResult ar)
    {
        TcpListener listener = (TcpListener)ar.AsyncState;

        clients.Add(new ServerClient(listener.EndAcceptTcpClient(ar)));
        StartListening();

        // Send a message to everyone, say someone has connected
        //Broadcast(clients[clients.Count - 1].clientName + " has connected", clients);
        Broadcast("%NAME",new List<ServerClient>() { clients[clients.Count - 1]});

    }


    private void Broadcast(string data, List<ServerClient> cl)
    {
        foreach (ServerClient c in cl)
        {
            try
            {
                StreamWriter writer = new StreamWriter(c.tcp.GetStream());
                writer.WriteLine(data);
                writer.Flush();
            }
            catch (Exception e)
            {
                Debug.Log("Write error: " + e.Message + " to client " + c.clientName);
            }
        }

    }

    private void OnIncomingData(ServerClient c, string data)
    {

        if(data.Contains("&NAME"))
        {
            c.clientName = data.Split('|')[1];
            Broadcast(c.clientName + "has connected", clients);
            return;
        }

        // TODO : Define protocol

        // send (clientA, clientB)'s or (clientB, clientA)'s  chat history
        if (data.Contains("history"))
        {
            
            //Broadcast(c.clientName + "has connected", clients);
            Debug.Log("chat history :");
            return;
        }


        Debug.Log(c.clientName + " has sent the followinng message : " + data);
        Broadcast(c.clientName + " : " + data, clients);
    }



    //private string GetChatHistory(string clientA, string clientB)
    //{

    //    StreamReader reader = new StreamReader()
    //    string chatHistory

    //    return chatHistory;
    //}


}

public class ServerClient
{
    public TcpClient tcp;
    public string clientName;

    public ServerClient(TcpClient clientSocket)
    {
        clientName = "Guest";
        tcp = clientSocket;
    }

}